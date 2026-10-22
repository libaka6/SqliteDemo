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

namespace SqliteDemo
{
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

        // ==================== 生命周期 ====================

        public Form1()
        {
            InitializeComponent();

            // 事件绑定（集中在这里，不用去 Designer 里翻）
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
            this.dataGridView1.CellClick += dataGridView1_CellClick;

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
            txtIP.Text = "127.0.0.1";      // IP 框默认值
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _running = false;      // 先停采集循环（让 async 循环自然退出）
            DisconnectMaster();    // 再断 Modbus 连接
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
        private bool CollectAndSave()
        {
            if (!_connected || _master == null) return false;
            try
            {
                ushort[] r = _master.ReadHoldingRegisters(1, 0, 10);
                float val = ToFloat(r[0], r[1]);                       // 读 float 模拟量
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                InsertRecord(time, val, "自动采集");
                return true;
            }
            catch
            {
                _connected = false;   // 读失败 = 断线，标记状态
                return false;
            }
        }

        // 采集循环（async 串行：做完一拍再做下一拍，天然不重叠）
        private async Task AcquisitionLoopAsync()
        {
            while (_running)
            {
                // 采集+入库放线程池；await 之后自动回到 UI 线程
                bool ok = await Task.Run(() => CollectAndSave());

                if (ok)
                {
                    ShowStatus("● 采集中，已入库", Color.Green);
                    LoadData();          // 回到 UI 线程后才刷新表格
                }
                else
                {
                    ShowStatus("● 采集/入库失败", Color.Red);
                }

                await Task.Delay(1000);  // 1 秒一拍
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

        private void ShowStatus(string text, Color color)
        {
            lblStatus.Text = text;
            lblStatus.ForeColor = color;
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
                await AcquisitionLoopAsync();   // 启动循环，直到 _running=false 才返回
            }
            else
            {
                _running = false;
                btnStart.Text = "开始采集";
                ShowStatus("● 已停止采集", Color.Gray);
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
    }
}