# SqliteDemo — C# 工业数据采集与监控系统

基于 **C# WinForms + NModbus4 + SQLite** 的工业数据采集与监控系统，实现 Modbus 设备实时采集、数据本地存储、历史条件查询与分页回看。

## 技术栈

- .NET Framework 4.8 + WinForms
- NModbus4（Modbus TCP 通信）
- System.Data.SQLite（本地数据库）
- async/await + Task.Run（后台采集入库，界面不卡顿）

## 功能特性

- **设备通信**：Modbus TCP 连接，2 秒超时保护，读写异常检测断线
- **实时采集**：每秒读取保持寄存器，按大小端（字节序）解析为 float 模拟量
- **数据存储**：采集数据带时间戳自动写入 SQLite，支持事务（Transaction）批量插入
- **历史查询**：按时间范围、备注关键字进行条件查询（参数化，防注入）
- **分页展示**：DataGridView 数据绑定，LIMIT + COUNT 分页，点行自动回填
- **后台任务**：采集与入库在后台线程执行，async 循环串行防重叠，界面始终流畅

## 系统数据流

```
设备 (Modbus TCP)
   → 读取保持寄存器 ReadHoldingRegisters
   → ToFloat 大小端解析
   → 带时间戳写入 SQLite
   → DataGridView 条件查询 / 分页回看
```

## 数据库设计

```sql
CREATE TABLE DataRecord (
    Id         INTEGER PRIMARY KEY AUTOINCREMENT,  -- 编号（只增不减）
    RecordTime TEXT NOT NULL,                      -- 采集时间 yyyy-MM-dd HH:mm:ss
    Value      REAL,                               -- 采集数值
    Remark     TEXT                                -- 备注
);
```

## 快速开始

1. 用 Visual Studio 2022 打开 `SqliteDemo.sln`，还原 NuGet 包（NModbus4、System.Data.SQLite）；
2. 启动 Modbus Slave：TCP / 端口 502 / Slave ID 1，显示模式选 Float AB CD；
3. 运行程序 → 输入 IP 点"连接" → 点"开始采集"，表格每秒新增一条记录。

## 项目结构

```
SqliteDemo/
├── Form1.cs            # 主界面（分层：字段 / 生命周期 / 数据库层 / 通信层 / 事件）
├── Form1.Designer.cs   # 界面设计
├── DataRecord.cs       # 数据记录模型
├── App.config          # 配置
└── SqliteDemo.csproj
```

## 核心知识点

- Modbus TCP 读写、四类数据区、功能码（03/06/10）
- 大小端 / 字节序解析（一个 32 位 float 占 2 个 16 位寄存器）
- SQLite 参数化增删改查、事务批量插入、连接即开即关
- async/await 后台采集、Task.Run、跨线程 UI 更新
- DataGridView 数据绑定（DataPropertyName）、条件查询、LIMIT 分页
