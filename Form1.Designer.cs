namespace SqliteDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.grpConnection = new System.Windows.Forms.GroupBox();
            this.txtLowLimit = new System.Windows.Forms.TextBox();
            this.txtHighLimit = new System.Windows.Forms.TextBox();
            this.lblAlarmState = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.layoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.layoutContent = new System.Windows.Forms.TableLayoutPanel();
            this.layoutLeft = new System.Windows.Forms.TableLayoutPanel();
            this.grpManual = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.grpRealtime = new System.Windows.Forms.GroupBox();
            this.lblFailCount = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblCollectCount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblRunDuration = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblLatestTime = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblRunStatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCurrentValue = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.layoutRight = new System.Windows.Forms.TableLayoutPanel();
            this.tabData = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.layoutTab1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelPage = new System.Windows.Forms.Panel();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvAlarm = new System.Windows.Forms.DataGridView();
            this.colAlarmId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlarmTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlarmValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlarmType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.grpLog = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.chkTime = new System.Windows.Forms.CheckBox();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.grpQuery = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnBatchDelete = new System.Windows.Forms.Button();
            this.grpConnection.SuspendLayout();
            this.layoutMain.SuspendLayout();
            this.layoutContent.SuspendLayout();
            this.layoutLeft.SuspendLayout();
            this.grpManual.SuspendLayout();
            this.grpRealtime.SuspendLayout();
            this.layoutRight.SuspendLayout();
            this.tabData.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.layoutTab1.SuspendLayout();
            this.panelPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlarm)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.grpLog.SuspendLayout();
            this.grpQuery.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpConnection
            // 
            this.grpConnection.Controls.Add(this.txtLowLimit);
            this.grpConnection.Controls.Add(this.txtHighLimit);
            this.grpConnection.Controls.Add(this.lblAlarmState);
            this.grpConnection.Controls.Add(this.label10);
            this.grpConnection.Controls.Add(this.label7);
            this.grpConnection.Controls.Add(this.lblStatus);
            this.grpConnection.Controls.Add(this.btnConnect);
            this.grpConnection.Controls.Add(this.txtIP);
            this.grpConnection.Controls.Add(this.btnStart);
            this.grpConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpConnection.Location = new System.Drawing.Point(13, 13);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Size = new System.Drawing.Size(1383, 53);
            this.grpConnection.TabIndex = 0;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "连接控制";
            // 
            // txtLowLimit
            // 
            this.txtLowLimit.Location = new System.Drawing.Point(762, 27);
            this.txtLowLimit.Name = "txtLowLimit";
            this.txtLowLimit.Size = new System.Drawing.Size(100, 28);
            this.txtLowLimit.TabIndex = 56;
            this.txtLowLimit.Text = "2";
            // 
            // txtHighLimit
            // 
            this.txtHighLimit.Location = new System.Drawing.Point(552, 26);
            this.txtHighLimit.Name = "txtHighLimit";
            this.txtHighLimit.Size = new System.Drawing.Size(100, 28);
            this.txtHighLimit.TabIndex = 55;
            this.txtHighLimit.Text = "8";
            // 
            // lblAlarmState
            // 
            this.lblAlarmState.AutoSize = true;
            this.lblAlarmState.ForeColor = System.Drawing.Color.Black;
            this.lblAlarmState.Location = new System.Drawing.Point(896, 32);
            this.lblAlarmState.Name = "lblAlarmState";
            this.lblAlarmState.Size = new System.Drawing.Size(44, 18);
            this.lblAlarmState.TabIndex = 54;
            this.lblAlarmState.Text = "正常";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(685, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 18);
            this.label10.TabIndex = 53;
            this.label10.Text = "下限：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(484, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 18);
            this.label7.TabIndex = 52;
            this.label7.Text = "上限:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 29);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(89, 18);
            this.lblStatus.TabIndex = 51;
            this.lblStatus.Text = "● 未连接";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(228, 24);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 29);
            this.btnConnect.TabIndex = 49;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(107, 24);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(100, 28);
            this.txtIP.TabIndex = 48;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(346, 25);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(101, 28);
            this.btnStart.TabIndex = 50;
            this.btnStart.Text = "开始采集";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // layoutMain
            // 
            this.layoutMain.ColumnCount = 1;
            this.layoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutMain.Controls.Add(this.grpConnection, 0, 0);
            this.layoutMain.Controls.Add(this.layoutContent, 0, 1);
            this.layoutMain.Controls.Add(this.grpLog, 0, 2);
            this.layoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutMain.Location = new System.Drawing.Point(0, 0);
            this.layoutMain.Name = "layoutMain";
            this.layoutMain.Padding = new System.Windows.Forms.Padding(10);
            this.layoutMain.RowCount = 3;
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.layoutMain.Size = new System.Drawing.Size(1409, 753);
            this.layoutMain.TabIndex = 0;
            // 
            // layoutContent
            // 
            this.layoutContent.ColumnCount = 2;
            this.layoutContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 260F));
            this.layoutContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutContent.Controls.Add(this.layoutLeft, 0, 0);
            this.layoutContent.Controls.Add(this.layoutRight, 1, 0);
            this.layoutContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutContent.Location = new System.Drawing.Point(13, 72);
            this.layoutContent.Name = "layoutContent";
            this.layoutContent.RowCount = 1;
            this.layoutContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutContent.Size = new System.Drawing.Size(1383, 558);
            this.layoutContent.TabIndex = 1;
            // 
            // layoutLeft
            // 
            this.layoutLeft.ColumnCount = 1;
            this.layoutLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutLeft.Controls.Add(this.grpManual, 0, 1);
            this.layoutLeft.Controls.Add(this.grpRealtime, 0, 0);
            this.layoutLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutLeft.Location = new System.Drawing.Point(3, 3);
            this.layoutLeft.Name = "layoutLeft";
            this.layoutLeft.RowCount = 2;
            this.layoutLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.layoutLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.layoutLeft.Size = new System.Drawing.Size(254, 552);
            this.layoutLeft.TabIndex = 0;
            // 
            // grpManual
            // 
            this.grpManual.Controls.Add(this.label3);
            this.grpManual.Controls.Add(this.label2);
            this.grpManual.Controls.Add(this.label1);
            this.grpManual.Controls.Add(this.btnDelete);
            this.grpManual.Controls.Add(this.btnUpdate);
            this.grpManual.Controls.Add(this.btnInsert);
            this.grpManual.Controls.Add(this.txtId);
            this.grpManual.Controls.Add(this.txtRemark);
            this.grpManual.Controls.Add(this.txtValue);
            this.grpManual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpManual.Location = new System.Drawing.Point(3, 223);
            this.grpManual.Name = "grpManual";
            this.grpManual.Size = new System.Drawing.Size(248, 326);
            this.grpManual.TabIndex = 0;
            this.grpManual.TabStop = false;
            this.grpManual.Text = "手动操作";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 18);
            this.label3.TabIndex = 59;
            this.label3.Text = "id：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 58;
            this.label2.Text = "备注：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 57;
            this.label1.Text = "数值：";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(6, 285);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(89, 35);
            this.btnDelete.TabIndex = 56;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(6, 244);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(89, 35);
            this.btnUpdate.TabIndex = 55;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(6, 201);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(89, 37);
            this.btnInsert.TabIndex = 54;
            this.btnInsert.Text = "插入";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(6, 158);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(204, 28);
            this.txtId.TabIndex = 53;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(6, 103);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(204, 28);
            this.txtRemark.TabIndex = 52;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(6, 47);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(204, 28);
            this.txtValue.TabIndex = 51;
            // 
            // grpRealtime
            // 
            this.grpRealtime.Controls.Add(this.lblFailCount);
            this.grpRealtime.Controls.Add(this.label11);
            this.grpRealtime.Controls.Add(this.lblCollectCount);
            this.grpRealtime.Controls.Add(this.label9);
            this.grpRealtime.Controls.Add(this.lblRunDuration);
            this.grpRealtime.Controls.Add(this.label5);
            this.grpRealtime.Controls.Add(this.lblLatestTime);
            this.grpRealtime.Controls.Add(this.label8);
            this.grpRealtime.Controls.Add(this.lblRunStatus);
            this.grpRealtime.Controls.Add(this.label6);
            this.grpRealtime.Controls.Add(this.lblCurrentValue);
            this.grpRealtime.Controls.Add(this.label4);
            this.grpRealtime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRealtime.Location = new System.Drawing.Point(3, 3);
            this.grpRealtime.Name = "grpRealtime";
            this.grpRealtime.Size = new System.Drawing.Size(248, 214);
            this.grpRealtime.TabIndex = 1;
            this.grpRealtime.TabStop = false;
            this.grpRealtime.Text = "实时监控";
            // 
            // lblFailCount
            // 
            this.lblFailCount.AutoSize = true;
            this.lblFailCount.Location = new System.Drawing.Point(101, 181);
            this.lblFailCount.Name = "lblFailCount";
            this.lblFailCount.Size = new System.Drawing.Size(35, 18);
            this.lblFailCount.TabIndex = 11;
            this.lblFailCount.Text = "0次";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 181);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 18);
            this.label11.TabIndex = 10;
            this.label11.Text = "失败次数:";
            // 
            // lblCollectCount
            // 
            this.lblCollectCount.AutoSize = true;
            this.lblCollectCount.Location = new System.Drawing.Point(101, 152);
            this.lblCollectCount.Name = "lblCollectCount";
            this.lblCollectCount.Size = new System.Drawing.Size(35, 18);
            this.lblCollectCount.TabIndex = 9;
            this.lblCollectCount.Text = "0条";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 152);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 18);
            this.label9.TabIndex = 8;
            this.label9.Text = "本次采集:";
            // 
            // lblRunDuration
            // 
            this.lblRunDuration.AutoSize = true;
            this.lblRunDuration.Location = new System.Drawing.Point(97, 118);
            this.lblRunDuration.Name = "lblRunDuration";
            this.lblRunDuration.Size = new System.Drawing.Size(80, 18);
            this.lblRunDuration.TabIndex = 7;
            this.lblRunDuration.Text = "00:00:00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 18);
            this.label5.TabIndex = 6;
            this.label5.Text = "运行时长:";
            // 
            // lblLatestTime
            // 
            this.lblLatestTime.AutoSize = true;
            this.lblLatestTime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLatestTime.Location = new System.Drawing.Point(100, 85);
            this.lblLatestTime.Name = "lblLatestTime";
            this.lblLatestTime.Size = new System.Drawing.Size(36, 25);
            this.lblLatestTime.TabIndex = 5;
            this.lblLatestTime.Text = "---";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 18);
            this.label8.TabIndex = 4;
            this.label8.Text = "最新时间:";
            // 
            // lblRunStatus
            // 
            this.lblRunStatus.AutoSize = true;
            this.lblRunStatus.Location = new System.Drawing.Point(97, 51);
            this.lblRunStatus.Name = "lblRunStatus";
            this.lblRunStatus.Size = new System.Drawing.Size(62, 18);
            this.lblRunStatus.TabIndex = 3;
            this.lblRunStatus.Text = "未运行";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 18);
            this.label6.TabIndex = 2;
            this.label6.Text = "运行状态:";
            // 
            // lblCurrentValue
            // 
            this.lblCurrentValue.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrentValue.Location = new System.Drawing.Point(77, 20);
            this.lblCurrentValue.Name = "lblCurrentValue";
            this.lblCurrentValue.Size = new System.Drawing.Size(46, 26);
            this.lblCurrentValue.TabIndex = 1;
            this.lblCurrentValue.Text = "---";
            this.lblCurrentValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "当前值:";
            // 
            // layoutRight
            // 
            this.layoutRight.ColumnCount = 1;
            this.layoutRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRight.Controls.Add(this.tabData, 0, 1);
            this.layoutRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutRight.Location = new System.Drawing.Point(263, 3);
            this.layoutRight.Name = "layoutRight";
            this.layoutRight.RowCount = 3;
            this.layoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutRight.Size = new System.Drawing.Size(1117, 552);
            this.layoutRight.TabIndex = 1;
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.tabPage1);
            this.tabData.Controls.Add(this.tabPage2);
            this.tabData.Controls.Add(this.tabPage3);
            this.tabData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabData.Location = new System.Drawing.Point(3, 3);
            this.tabData.Name = "tabData";
            this.tabData.SelectedIndex = 0;
            this.tabData.Size = new System.Drawing.Size(1111, 546);
            this.tabData.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.layoutTab1);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1103, 514);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "采集记录";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // layoutTab1
            // 
            this.layoutTab1.ColumnCount = 1;
            this.layoutTab1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutTab1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTab1.Controls.Add(this.panelPage, 0, 2);
            this.layoutTab1.Controls.Add(this.grpQuery, 0, 0);
            this.layoutTab1.Controls.Add(this.dataGridView1, 0, 1);
            this.layoutTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTab1.Location = new System.Drawing.Point(3, 3);
            this.layoutTab1.Name = "layoutTab1";
            this.layoutTab1.RowCount = 3;
            this.layoutTab1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.layoutTab1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutTab1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.layoutTab1.Size = new System.Drawing.Size(1097, 508);
            this.layoutTab1.TabIndex = 0;
            // 
            // panelPage
            // 
            this.panelPage.Controls.Add(this.lblPageInfo);
            this.panelPage.Controls.Add(this.btnNext);
            this.panelPage.Controls.Add(this.btnPrev);
            this.panelPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPage.Location = new System.Drawing.Point(3, 471);
            this.panelPage.Name = "panelPage";
            this.panelPage.Size = new System.Drawing.Size(1091, 34);
            this.panelPage.TabIndex = 50;
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.Location = new System.Drawing.Point(322, 8);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(188, 18);
            this.lblPageInfo.TabIndex = 46;
            this.lblPageInfo.Text = "第 1 / 1 页，共 0 条";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(516, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(127, 31);
            this.btnNext.TabIndex = 44;
            this.btnNext.Text = "下一页";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(190, 3);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(126, 28);
            this.btnPrev.TabIndex = 43;
            this.btnPrev.Text = "上一页";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colTime,
            this.colValue,
            this.colRemark});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 63);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1091, 402);
            this.dataGridView1.TabIndex = 48;
            // 
            // colId
            // 
            this.colId.DataPropertyName = "Id";
            this.colId.HeaderText = "编号";
            this.colId.MinimumWidth = 8;
            this.colId.Name = "colId";
            this.colId.Width = 150;
            // 
            // colTime
            // 
            this.colTime.DataPropertyName = "RecordTime";
            this.colTime.HeaderText = "采集时间";
            this.colTime.MinimumWidth = 8;
            this.colTime.Name = "colTime";
            this.colTime.Width = 150;
            // 
            // colValue
            // 
            this.colValue.DataPropertyName = "Value";
            this.colValue.HeaderText = "数值";
            this.colValue.MinimumWidth = 8;
            this.colValue.Name = "colValue";
            this.colValue.Width = 150;
            // 
            // colRemark
            // 
            this.colRemark.DataPropertyName = "Remark";
            this.colRemark.HeaderText = "备注";
            this.colRemark.MinimumWidth = 8;
            this.colRemark.Name = "colRemark";
            this.colRemark.Width = 150;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvAlarm);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(824, 514);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "报警记录";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvAlarm
            // 
            this.dgvAlarm.AllowUserToAddRows = false;
            this.dgvAlarm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlarm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAlarmId,
            this.colAlarmTime,
            this.colAlarmValue,
            this.colAlarmType,
            this.colMessage});
            this.dgvAlarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlarm.Location = new System.Drawing.Point(3, 3);
            this.dgvAlarm.Name = "dgvAlarm";
            this.dgvAlarm.ReadOnly = true;
            this.dgvAlarm.RowHeadersWidth = 62;
            this.dgvAlarm.RowTemplate.Height = 30;
            this.dgvAlarm.Size = new System.Drawing.Size(818, 508);
            this.dgvAlarm.TabIndex = 0;
            // 
            // colAlarmId
            // 
            this.colAlarmId.DataPropertyName = "Id";
            this.colAlarmId.HeaderText = "编号";
            this.colAlarmId.MinimumWidth = 8;
            this.colAlarmId.Name = "colAlarmId";
            this.colAlarmId.ReadOnly = true;
            this.colAlarmId.Width = 150;
            // 
            // colAlarmTime
            // 
            this.colAlarmTime.DataPropertyName = "AlarmTime";
            this.colAlarmTime.HeaderText = "报警时间";
            this.colAlarmTime.MinimumWidth = 8;
            this.colAlarmTime.Name = "colAlarmTime";
            this.colAlarmTime.ReadOnly = true;
            this.colAlarmTime.Width = 150;
            // 
            // colAlarmValue
            // 
            this.colAlarmValue.DataPropertyName = "Value";
            this.colAlarmValue.HeaderText = "报警值";
            this.colAlarmValue.MinimumWidth = 8;
            this.colAlarmValue.Name = "colAlarmValue";
            this.colAlarmValue.ReadOnly = true;
            this.colAlarmValue.Width = 150;
            // 
            // colAlarmType
            // 
            this.colAlarmType.DataPropertyName = "AlarmType";
            this.colAlarmType.HeaderText = "报警类型";
            this.colAlarmType.MinimumWidth = 8;
            this.colAlarmType.Name = "colAlarmType";
            this.colAlarmType.ReadOnly = true;
            this.colAlarmType.Width = 150;
            // 
            // colMessage
            // 
            this.colMessage.DataPropertyName = "Message";
            this.colMessage.HeaderText = "报警信息";
            this.colMessage.MinimumWidth = 8;
            this.colMessage.Name = "colMessage";
            this.colMessage.ReadOnly = true;
            this.colMessage.Width = 150;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cartesianChart1);
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(824, 514);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "实时曲线";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartesianChart1.Location = new System.Drawing.Point(3, 3);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(818, 508);
            this.cartesianChart1.TabIndex = 0;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // grpLog
            // 
            this.grpLog.Controls.Add(this.txtLog);
            this.grpLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLog.Location = new System.Drawing.Point(13, 636);
            this.grpLog.Name = "grpLog";
            this.grpLog.Size = new System.Drawing.Size(1383, 104);
            this.grpLog.TabIndex = 2;
            this.grpLog.TabStop = false;
            this.grpLog.Text = "运行日志";
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(3, 24);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(1377, 77);
            this.txtLog.TabIndex = 34;
            // 
            // chkTime
            // 
            this.chkTime.AutoSize = true;
            this.chkTime.Location = new System.Drawing.Point(2, 26);
            this.chkTime.Name = "chkTime";
            this.chkTime.Size = new System.Drawing.Size(124, 22);
            this.chkTime.TabIndex = 38;
            this.chkTime.Text = "按时间过滤";
            this.chkTime.UseVisualStyleBackColor = true;
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(115, 20);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.ShowUpDown = true;
            this.dtpStart.Size = new System.Drawing.Size(201, 28);
            this.dtpStart.TabIndex = 39;
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(309, 20);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.ShowUpDown = true;
            this.dtpEnd.Size = new System.Drawing.Size(201, 28);
            this.dtpEnd.TabIndex = 40;
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(516, 20);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(100, 28);
            this.txtKeyword.TabIndex = 41;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(622, 22);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(89, 28);
            this.btnQuery.TabIndex = 42;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(717, 24);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(128, 26);
            this.btnRefresh.TabIndex = 42;
            this.btnRefresh.Text = "刷新（全部）";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // grpQuery
            // 
            this.grpQuery.Controls.Add(this.btnBatchDelete);
            this.grpQuery.Controls.Add(this.btnExport);
            this.grpQuery.Controls.Add(this.btnRefresh);
            this.grpQuery.Controls.Add(this.btnQuery);
            this.grpQuery.Controls.Add(this.txtKeyword);
            this.grpQuery.Controls.Add(this.dtpEnd);
            this.grpQuery.Controls.Add(this.dtpStart);
            this.grpQuery.Controls.Add(this.chkTime);
            this.grpQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpQuery.Location = new System.Drawing.Point(3, 3);
            this.grpQuery.Name = "grpQuery";
            this.grpQuery.Size = new System.Drawing.Size(1091, 54);
            this.grpQuery.TabIndex = 49;
            this.grpQuery.TabStop = false;
            this.grpQuery.Text = "历史查询";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(851, 25);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(104, 23);
            this.btnExport.TabIndex = 43;
            this.btnExport.Text = "导出 Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnBatchDelete
            // 
            this.btnBatchDelete.Location = new System.Drawing.Point(961, 24);
            this.btnBatchDelete.Name = "btnBatchDelete";
            this.btnBatchDelete.Size = new System.Drawing.Size(102, 23);
            this.btnBatchDelete.TabIndex = 44;
            this.btnBatchDelete.Text = "批量删除";
            this.btnBatchDelete.UseVisualStyleBackColor = true;
            this.btnBatchDelete.Click += new System.EventHandler(this.btnBatchDelete_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1409, 753);
            this.Controls.Add(this.layoutMain);
            this.Name = "Form1";
            this.Text = "Form1";
            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.layoutMain.ResumeLayout(false);
            this.layoutContent.ResumeLayout(false);
            this.layoutLeft.ResumeLayout(false);
            this.grpManual.ResumeLayout(false);
            this.grpManual.PerformLayout();
            this.grpRealtime.ResumeLayout(false);
            this.grpRealtime.PerformLayout();
            this.layoutRight.ResumeLayout(false);
            this.tabData.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.layoutTab1.ResumeLayout(false);
            this.panelPage.ResumeLayout(false);
            this.panelPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlarm)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.grpLog.ResumeLayout(false);
            this.grpLog.PerformLayout();
            this.grpQuery.ResumeLayout(false);
            this.grpQuery.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpConnection;
        private System.Windows.Forms.TableLayoutPanel layoutMain;
        private System.Windows.Forms.TableLayoutPanel layoutContent;
        private System.Windows.Forms.GroupBox grpLog;
        private System.Windows.Forms.TableLayoutPanel layoutLeft;
        private System.Windows.Forms.GroupBox grpManual;
        private System.Windows.Forms.TableLayoutPanel layoutRight;
        private System.Windows.Forms.GroupBox grpRealtime;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblRunStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCurrentValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblLatestTime;
        private System.Windows.Forms.Label lblRunDuration;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFailCount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblCollectCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLowLimit;
        private System.Windows.Forms.TextBox txtHighLimit;
        private System.Windows.Forms.Label lblAlarmState;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tabData;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel layoutTab1;
        private System.Windows.Forms.Panel panelPage;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemark;
        private System.Windows.Forms.DataGridView dgvAlarm;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlarmId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlarmTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlarmValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlarmType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMessage;
        private System.Windows.Forms.TabPage tabPage3;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.GroupBox grpQuery;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.CheckBox chkTime;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnBatchDelete;
    }
}

