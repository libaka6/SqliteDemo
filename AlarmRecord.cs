using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteDemo
{
    internal class AlarmRecord
    {
        public int Id { get; set; }
        public string AlarmTime { get; set; }
        public double Value { get; set; }
        public string AlarmType { get; set; }   // 高报 / 低报
        public string Message { get; set; }
    }
}
