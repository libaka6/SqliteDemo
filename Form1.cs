using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using LiveCharts;
using LiveCharts.Wpf;
using Media = System.Windows.Media;   // 别名！防止和 System.Drawing 撞名
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace SqliteDemo
{
    //以下注释皆为ai生成，ai是真好用啊
    public partial class Form1 : Form
    {
        // ==================== 字段区 ====================

        // ----- 数据库 -----
        private string _dbPath;           // 数据库文件完整路径
        private string _connStr;          // 连接字符串

        // ----- 分页 -----
        private int _pageIndex = 1;        // 当前页，从 1 开始
        private const int PageSize = 10;   // 每页条数（const = 常量，不可改）
        private int _totalCount = 0;       // 符合条件的总条数
        private int _totalPages = 0;       // 总页数

        // ----- Modbus -----
        private TcpClient _tcpClient;
        private IModbusMaster _master;
        private bool _connected = false;   // Modbus 连接状态
        private bool _running = false;     // 采集循环是否运行
        // ----- 运行统计（D19） -----
        private Stopwatch _stopwatch;          // 高精度秒表
        private Timer _timerClock;             // 独立时钟（System.Windows.Forms.Timer）
        private int _collectCount = 0;         // 本次采集成功数
        private int _failCount = 0;            // 本次失败数
        // ----- 报警（D20） -----
        private bool _isAlarming = false;    // 状态记忆位：现在是否处于报警中
        private Timer _timerBlink;           // 闪烁定时器（WinForms Timer）
        private bool _blinkFlag = false;     // 闪烁翻转标志（红/灰交替）
        // ----- 实时曲线（D22） -----
        private SeriesCollection _chartSeries;          // 曲线集合
        private ChartValues<double> _chartValues;       // 数据点
        private List<string> _chartLabels;              // X 轴时间标签
        private const int MaxPoints = 50;               // 最多 50 个点

        // ==================== 生命周期 ====================

        public Form1()
        {
            InitializeComponent();

            // 事件绑定（集中在这里，不用去 Designer 里翻）
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
            this.dataGridView1.CellClick += dataGridView1_CellClick;
            // 独立时钟：每秒刷新一次运行时长（Tick 在 UI 线程，直接改控件）
            _timerClock = new Timer();
            _timerClock.Interval = 1000;
            _timerClock.Tick += TimerClock_Tick;
            // 闪烁定时器：报警时红灰交替（Tick 在 UI 线程，直接改颜色）
            _timerBlink = new Timer();
            _timerBlink.Interval = 500;
            _timerBlink.Tick += TimerBlink_Tick;

            // 表格设置
            dataGridView1.AutoGenerateColumns = false;   // 防中英双份列
            dataGridView1.ReadOnly = true;               // 历史数据只读
            dataGridView1.AllowUserToAddRows = false;    // 去掉底部空白行

            // 列绑定（DataPropertyName 与模型属性一字不差）
            colId.DataPropertyName = "Id";
            colTime.DataPropertyName = "RecordTime";
            colValue.DataPropertyName = "Value";
            colRemark.DataPropertyName = "Remark";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 库文件放在 exe 同目录（bin\Debug），名字 data.db
            _dbPath = Path.Combine(Application.StartupPath, "data.db");
            _connStr = "Data Source=" + _dbPath + ";Version=3;";

            txtLog.AppendText("数据库路径：" + _dbPath + "\r\n");
            InitDb();                      // 启动即建表
            LoadData();                    // 启动即加载表格
            LoadAlarms();                  // 启动即加载报警记录
            txtIP.Text = "127.0.0.1";      // IP 框默认值
                                           // 初始化实时曲线（D22）
            _chartValues = new ChartValues<double>();
            _chartLabels = new List<string>();

            LineSeries line = new LineSeries
            {
                Title = "采集值",
                Values = _chartValues,
                PointGeometry = null,              // 只画线不画点圈，更流畅
                StrokeThickness = 2,
                Fill = Media.Brushes.Transparent   // 别名 Media.，去面积裙摆
            };

            _chartSeries = new SeriesCollection { line };
            cartesianChart1.Series = _chartSeries;

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "时间",
                Labels = _chartLabels,
                Separator = new Separator { Step = 10 }   // 每 10 点标一个，防挤
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "值",
                LabelFormatter = value => value.ToString("F1")
            });

            cartesianChart1.LegendLocation = LegendLocation.Top;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _running = false;      // 先停采集循环（让 async 循环自然退出）
            DisconnectMaster();    // 再断 Modbus 连接
            ResetAlarmState();
        }

        // ==================== 数据库层 ====================

        // 建库 + 建表（幂等，反复启动不报错）
        private void InitDb()
        {
            // Open 时若 data.db 不存在，会自动创建文件
            using (SQLiteConnection conn = new SQLiteConnection(_connStr))
            {
                conn.Open();
                string sql = @"CREATE TABLE IF NOT EXISTS DataRecord (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            RecordTime TEXT NOT NULL,
            Value REAL,
            Remark TEXT
        );";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();   // 执行建表，不返回结果集
                }
                // 报警记录表（D20）
                string sqlAlarm = @"CREATE TABLE IF NOT EXISTS AlarmRecord (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            AlarmTime TEXT NOT NULL,
            Value REAL,
            AlarmType TEXT,
            Message TEXT
        );";
                using (SQLiteCommand cmd2 = new SQLiteCommand(sqlAlarm, conn))
                {
                    cmd2.ExecuteNonQuery();
                }
            }
            txtLog.AppendText("已就绪：数据库与表已创建\r\n");
        }

        // 插入一条记录；time/val/remark 全部参数传入，不依赖 UI 控件（后台线程可用）
        private int InsertRecord(string time, double val, string remark)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_connStr))
            {
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO DataRecord(RecordTime,Value,Remark) VALUES(@time,@val,@remark);";
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Parameters.AddWithValue("@val", val);
                    cmd.Parameters.AddWithValue("@remark", (object)remark ?? DBNull.Value);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // 事务批量插入：全部成功才 Commit，中途异常自动回滚（批量补传/导入用）
        private void InsertBatch(List<DataRecord> records)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_connStr))
            {
                conn.Open();
                using (SQLiteTransaction tran = conn.BeginTransaction())
                {
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = tran;
                        cmd.CommandText = "INSERT INTO DataRecord(RecordTime,Value,Remark) VALUES(@time,@val,@remark);";
                        var pTime = cmd.Parameters.Add("@time", DbType.String);
                        var pVal = cmd.Parameters.Add("@val", DbType.Double);
                        var pRemark = cmd.Parameters.Add("@remark", DbType.String);

                        foreach (var rec in records)
                        {
                            pTime.Value = rec.RecordTime;
                            pVal.Value = rec.Value;
                            pRemark.Value = (object)rec.Remark ?? DBNull.Value;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tran.Commit();   // 只在最后刷一次盘
                }
            }
        }
        // ==================== 报警层 ====================

        // 阈值判断：超上限→"高报"，低于下限→"低报"，正常→null
        private string CheckAlarm(float val)
        {
            if (double.TryParse(txtHighLimit.Text, out double high) && val > high)
                return "高报";
            if (double.TryParse(txtLowLimit.Text, out double low) && val < low)
                return "低报";
            return null;
        }

        // 写一条报警记录（参数化，风格与 InsertRecord 一致）
        private int InsertAlarm(string time, double val, string type, string message)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_connStr))
            {
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO AlarmRecord(AlarmTime,Value,AlarmType,Message) VALUES(@time,@val,@type,@msg);";
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Parameters.AddWithValue("@val", val);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@msg", message);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // 加载报警记录到 dgvAlarm（启动时 + 每次新报警后调用）
        private void LoadAlarms()
        {
            List<AlarmRecord> list = new List<AlarmRecord>();
            using (SQLiteConnection conn = new SQLiteConnection(_connStr))
            {
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id,AlarmTime,Value,AlarmType,Message FROM AlarmRecord ORDER BY Id DESC;";
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new AlarmRecord
                            {
                                Id = reader.GetInt32(0),
                                AlarmTime = reader.GetString(1),
                                Value = reader.GetDouble(2),
                                AlarmType = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                Message = reader.IsDBNull(4) ? "" : reader.GetString(4)
                            });
                        }
                    }
                }
            }
            dgvAlarm.DataSource = list;
        }

        // 根据界面控件构造 WHERE 子句；拼接的是固定 SQL 片段，用户输入都进参数
        private string BuildWhere(SQLiteCommand cmd)
        {
            string where = " WHERE 1=1 ";

            if (chkTime.Checked)
            {
                where += " AND RecordTime >= @start AND RecordTime <= @end ";
                cmd.Parameters.AddWithValue("@start", dtpStart.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@end", dtpEnd.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            if (!string.IsNullOrWhiteSpace(txtKeyword.Text))
            {
                where += " AND Remark LIKE @kw ";
                cmd.Parameters.AddWithValue("@kw", "%" + txtKeyword.Text + "%");
            }

            return where;
        }

        // 按当前条件 + 当前页查询并绑定表格
        private void LoadData()
        {
            List<DataRecord> list = new List<DataRecord>();

            using (SQLiteConnection conn = new SQLiteConnection(_connStr))
            {
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    string where = BuildWhere(cmd);

                    // ① COUNT 拿总条数（ExecuteScalar 取第一行第一列）
                    cmd.CommandText = "SELECT COUNT(*) FROM DataRecord " + where;
                    _totalCount = Convert.ToInt32(cmd.ExecuteScalar());
                    _totalPages = (_totalCount + PageSize - 1) / PageSize;   // 向上取整
                    if (_pageIndex > _totalPages) _pageIndex = Math.Max(1, _totalPages);

                    // ② 取当前页：LIMIT 偏移量,条数
                    int offset = (_pageIndex - 1) * PageSize;
                    cmd.CommandText = "SELECT Id,RecordTime,Value,Remark FROM DataRecord " + where
                        + " ORDER BY Id DESC LIMIT " + offset + "," + PageSize + ";";

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new DataRecord
                            {
                                Id = reader.GetInt32(0),
                                RecordTime = reader.GetString(1),
                                Value = reader.GetDouble(2),
                                Remark = reader.IsDBNull(3) ? "" : reader.GetString(3)
                            });
                        }
                    }
                }
            }

            dataGridView1.DataSource = list;   // 绑定表格
            UpdatePageInfo();
        }
        // 按当前查询条件取【全部】记录（不分页，专供导出）
        private List<DataRecord> QueryAllRecords()
        {
            List<DataRecord> list = new List<DataRecord>();
            using (SQLiteConnection conn = new SQLiteConnection(_connStr))
            {
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    string where = BuildWhere(cmd);   // 复用查询条件（含参数！）
                    cmd.CommandText = "SELECT Id,RecordTime,Value,Remark FROM DataRecord " + where
                        + " ORDER BY Id ASC;";        // 导出按时间正序（老→新），账本从头读
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new DataRecord
                            {
                                Id = reader.GetInt32(0),
                                RecordTime = reader.GetString(1),
                                Value = reader.GetDouble(2),
                                Remark = reader.IsDBNull(3) ? "" : reader.GetString(3)
                            });
                        }
                    }
                }
            }
            return list;
        }
        // 把记录写成 xlsx 文件
        private void ExportToExcel(string path, List<DataRecord> records)
        {
            IWorkbook workbook = new XSSFWorkbook();      // 建册（xlsx 用 XSSF）
            ISheet sheet = workbook.CreateSheet("采集记录");   // 建表

            // 表头（第 0 行）
            IRow header = sheet.CreateRow(0);
            header.CreateCell(0).SetCellValue("编号");
            header.CreateCell(1).SetCellValue("采集时间");
            header.CreateCell(2).SetCellValue("数值");
            header.CreateCell(3).SetCellValue("备注");

            // 数据（从第 1 行开始）
            for (int i = 0; i < records.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(records[i].Id);
                row.CreateCell(1).SetCellValue(records[i].RecordTime);
                row.CreateCell(2).SetCellValue(records[i].Value);
                row.CreateCell(3).SetCellValue(records[i].Remark);
            }

            sheet.SetColumnWidth(1, 20 * 256);   // 时间列加宽（单位 1/256 字符）
            sheet.SetColumnWidth(3, 20 * 256);

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                workbook.Write(fs);   // 写完 fs 自动释放（否则文件被占用打不开）
            }
        }

        // 更新页码标签和按钮可用状态
        private void UpdatePageInfo()
        {
            lblPageInfo.Text = "第 " + _pageIndex + " / " + Math.Max(1, _totalPages)
                             + " 页，共 " + _totalCount + " 条";
            btnPrev.Enabled = _pageIndex > 1;
            btnNext.Enabled = _pageIndex < _totalPages;
        }

        // ==================== 通信层（Modbus） ====================

        // 建立连接（成功返回 true，含 2 秒超时保护）
        private bool ConnectMaster(string ip, int port)
        {
            try
            {
                _tcpClient?.Close();
                _tcpClient = new TcpClient();
                _tcpClient.ReceiveTimeout = 2000;
                _tcpClient.SendTimeout = 2000;
                var task = _tcpClient.ConnectAsync(ip, port);
                if (!task.Wait(2000)) { _tcpClient.Close(); _connected = false; return false; }
                _master = ModbusIpMaster.CreateIp(_tcpClient);
                _connected = true;
                return true;
            }
            catch { _connected = false; return false; }
        }

        // 断开并释放
        private void DisconnectMaster()
        {
            _connected = false;
            _master = null;
            _tcpClient?.Close();
        }

        // 后台线程执行：读 Modbus float → 带时间戳入库；成功返回 true
        private float? CollectAndSave()
        {
            if (!_connected || _master == null) return null;
            try
            {
                ushort[] r = _master.ReadHoldingRegisters(1, 0, 10);
                float val = ToFloat(r[0], r[1]);                       // 读 float 模拟量
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                InsertRecord(time, val, "自动采集");
                return val;
            }
            catch
            {
                _connected = false;   // 读失败 = 断线，标记状态
                return null;
            }
        }

        // 采集循环（async 串行：做完一拍再做下一拍，天然不重叠）
        private async Task AcquisitionLoopAsync()
        {
            while (_running)
            {
                float? val = await Task.Run(() => CollectAndSave());
                if (!_running) break;   // 停止/断开后，丢弃这一拍（防回马枪）

                if (val.HasValue)
                {
                    lblCurrentValue.Text = val.Value.ToString("F2");
                    lblRunStatus.Text = "采集中";
                    _collectCount++;
                    lblCollectCount.Text = _collectCount + "条";
                    lblLatestTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    ShowStatus("● 采集中，已入库", Color.Green);
                    AddChartPoint(val.Value);
                    // 报警状态切换（边沿触发）：只在状态翻转那一刻记一次
                    string alarm = CheckAlarm(val.Value);
                    if (alarm != null)
                    {
                        if (!_isAlarming)   // 正常 → 超限：这一拍记录
                        {
                            _isAlarming = true;
                            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            string msg = alarm + "，当前值 " + val.Value.ToString("F2");
                            InsertAlarm(now, val.Value, alarm, msg);
                            _timerBlink.Start();
                            lblAlarmState.Text = "● " + alarm;
                            LoadAlarms();
                        }
                        // 一直超限：不重复写记录（_isAlarming 记忆住了）
                    }
                    else
                    {
                        if (_isAlarming)    // 超限 → 恢复：解除报警
                        {
                            ResetAlarmState();
                        }
                    }
                    LoadData();
                }
                else
                {
                    lblRunStatus.Text = "采集失败";
                    ShowStatus("● 采集/入库失败", Color.Red);
                    _failCount++;
                    lblFailCount.Text = _failCount + " 次";
                }
                await Task.Delay(1000);   // 1 秒一拍
            }
        }

        // 两个寄存器按"高字在前（大端）"拼成 float
        private float ToFloat(ushort high, ushort low)
        {
            byte[] b = new byte[4];
            b[0] = (byte)(high >> 8); b[1] = (byte)(high & 0xFF);
            b[2] = (byte)(low >> 8); b[3] = (byte)(low & 0xFF);
            if (BitConverter.IsLittleEndian) Array.Reverse(b);
            return BitConverter.ToSingle(b, 0);
        }

        // 两个寄存器拼成 32 位有符号整数（大端）
        private int ToInt32(ushort high, ushort low) { return (high << 16) | low; }

        // ==================== 辅助方法 ====================
        // 时钟心跳：每秒把秒表读数写上"黑板"（UI 线程）
        private void TimerClock_Tick(object sender, EventArgs e)
        {
            if (_stopwatch == null) return;   // 防御：秒表还没造好就不看
            TimeSpan ts = _stopwatch.Elapsed;
            // 超过 24 小时用 TotalHours，否则 Hours 归零会显示乱套
            lblRunDuration.Text = string.Format("{0:00}:{1:00}:{2:00}",
                (int)ts.TotalHours, ts.Minutes, ts.Seconds);
        }
        // 闪烁心跳：每 500ms 翻转一次颜色（报警时才启动）
        private void TimerBlink_Tick(object sender, EventArgs e)
        {
            _blinkFlag = !_blinkFlag;   // 翻面
            Color c = _blinkFlag ? Color.Red : Color.Gray;
            lblCurrentValue.ForeColor = c;
            lblAlarmState.ForeColor = c;
        }
        // 复位报警状态机（停止/断开/关窗时调用——"成对原则"的提炼）
        private void ResetAlarmState()
        {
            _timerBlink?.Stop();
            _isAlarming = false;
            _blinkFlag = false;
            lblAlarmState.Text = "正常";
            lblAlarmState.ForeColor = Color.Black;
            lblCurrentValue.ForeColor = Color.Black;
        }
        private void ShowStatus(string text, Color color)
        {
            lblStatus.Text = text;
            lblStatus.ForeColor = color;
        }
        // 加一个点：加新删旧实现滚动，点和时间标签同进同退
        private void AddChartPoint(double val)
        {
            _chartValues.Add(val);
            _chartLabels.Add(DateTime.Now.ToString("HH:mm:ss"));

            if (_chartValues.Count > MaxPoints)
            {
                _chartValues.RemoveAt(0);    // 删最老的点
                _chartLabels.RemoveAt(0);    // 同步删时间标签
            }
            cartesianChart1.AxisX[0].Labels = _chartLabels;
        }
        // ==================== 按钮事件 ====================

        // 连接 / 断开 Modbus
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!_connected)
            {
                if (ConnectMaster(txtIP.Text, 502))
                {
                    btnConnect.Text = "断开";
                    ShowStatus("● 已连接", Color.Green);
                }
                else MessageBox.Show("连接失败，请确认 Modbus Slave 已开启 TCP/502");
            }
            else
            {
                _running = false;          // 先停采集，再断连接（顺序很重要）
                DisconnectMaster();
                btnConnect.Text = "连接";
                btnStart.Text = "开始采集";
                ShowStatus("● 已断开", Color.Gray);
                _timerClock.Stop();
                _stopwatch?.Stop();
                ResetAlarmState();
            }
        }

        // 开始 / 停止采集
        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (!_running)
            {
                if (!_connected) { MessageBox.Show("请先连接 Modbus"); return; }
                _running = true;
                btnStart.Text = "停止采集";
                _collectCount = 0;
                _failCount = 0;
                lblCollectCount.Text = "0条";
                lblFailCount.Text = "0次";
                _chartValues?.Clear();
                _chartLabels?.Clear();
                _stopwatch = Stopwatch.StartNew();// 秒表从零开始计时
                _timerClock.Start();// 启动独立时钟
                await AcquisitionLoopAsync();   // 启动循环，直到 _running=false 才返回
            }
            else
            {
                _running = false;
                btnStart.Text = "开始采集";
                ShowStatus("● 已停止采集", Color.Gray);
                lblRunStatus.Text = "已停止";
                _timerClock.Stop();
                _stopwatch.Stop();
                ResetAlarmState();
            }
        }

        // 查询（条件过滤 + 分页，换条件回第 1 页）
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (chkTime.Checked && dtpStart.Value > dtpEnd.Value)
            {
                MessageBox.Show("开始时间不能晚于结束时间"); return;
            }
            _pageIndex = 1;
            LoadData();
        }

        // 刷新（清条件、回第 1 页、全量显示）
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            chkTime.Checked = false;
            txtKeyword.Clear();
            _pageIndex = 1;
            LoadData();
        }

        // 上一页 / 下一页
        private void btnPrev_Click(object sender, EventArgs e) { _pageIndex--; LoadData(); }
        private void btnNext_Click(object sender, EventArgs e) { _pageIndex++; LoadData(); }

        // 手动插入一条（从输入框取值，委托 InsertRecord 执行）
        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtValue.Text, out double val))
            {
                MessageBox.Show("请输入有效的数值！数值必须是数字"); return;
            }
            string remark = string.IsNullOrEmpty(txtRemark.Text) ? null : txtRemark.Text;
            int rows = InsertRecord(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), val, remark);
            txtLog.AppendText("插入 " + rows + " 行\r\n");
            txtLog.ScrollToCaret();
        }

        // 更新指定 Id 的数值
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("请输入有效的 Id！Id 必须是整数");
                return;
            }
            if (!double.TryParse(txtValue.Text, out double val))
            {
                MessageBox.Show("数值必须是数字");
                return;
            }
            string sql = "UPDATE DataRecord SET Value=@val WHERE Id=@id;";
            using (SQLiteConnection conn = new SQLiteConnection(_connStr))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@val", val);
                    cmd.Parameters.AddWithValue("@id", id);
                    int rows = cmd.ExecuteNonQuery();
                    txtLog.AppendText("更新 " + rows + " 行\r\n");
                    txtLog.ScrollToCaret();
                }
            }
        }

        // 删除指定 Id 的记录
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Id 必须是整数");
                return;
            }

            string sql = "DELETE FROM DataRecord WHERE Id=@id;";

            using (SQLiteConnection conn = new SQLiteConnection(_connStr))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rows = cmd.ExecuteNonQuery();
                    txtLog.AppendText("删除 " + rows + " 行\r\n");
                    txtLog.ScrollToCaret();
                }
            }
        }

        // ==================== 表格事件 ====================

        // 点表格行，自动填输入框（给更新/删除用）
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dataGridView1.Rows[e.RowIndex];
            txtId.Text = row.Cells["colId"].Value.ToString();
            txtValue.Text = row.Cells["colValue"].Value.ToString();
            txtRemark.Text = row.Cells["colRemark"].Value.ToString();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            List<DataRecord> records = QueryAllRecords();
            if (records.Count == 0) { MessageBox.Show("没有可导出的数据"); return; }

            using (SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "Excel 文件|*.xlsx",
                FileName = "采集记录_" + DateTime.Now.ToString("yyyyMMdd_HHmmss")
            })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ExportToExcel(dlg.FileName, records);
                        MessageBox.Show("导出成功，共 " + records.Count + " 条");
                        txtLog.AppendText("导出 Excel：" + dlg.FileName + "\r\n");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出失败：" + ex.Message);
                    }
                }
            }
        }

        private void btnBatchDelete_Click(object sender, EventArgs e)
        {
            // ① 收集选中行的 Id
            List<int> ids = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Cells["colId"].Value != null)
                    ids.Add(Convert.ToInt32(row.Cells["colId"].Value));
            }

            if (ids.Count == 0)
            {
                MessageBox.Show("请先选中要删除的行（按住 Ctrl 可多选）");
                return;
            }

            // ② 二次确认（危险操作必弹，显示条数防误删）
            if (MessageBox.Show("确认删除选中的 " + ids.Count + " 条记录？此操作不可恢复",
                "批量删除确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            // ③ 参数化 IN 删除（一条 DELETE，天然原子）
            using (SQLiteConnection conn = new SQLiteConnection(_connStr))
            {
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    List<string> paras = new List<string>();
                    for (int i = 0; i < ids.Count; i++)
                    {
                        string p = "@id" + i;
                        paras.Add(p);
                        cmd.Parameters.AddWithValue(p, ids[i]);
                    }
                    cmd.CommandText = "DELETE FROM DataRecord WHERE Id IN (" + string.Join(",", paras) + ");";
                    cmd.ExecuteNonQuery();
                }
            }

            // ④ 反馈 + 刷新
            txtLog.AppendText("批量删除 " + ids.Count + " 条\r\n");
            LoadData();
        }
    }
}