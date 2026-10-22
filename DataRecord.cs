using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteDemo
{
   
    public class DataRecord
    {
        public int Id { get; set; }
        public string RecordTime { get; set; }
        public double Value { get; set; }
        public string Remark { get; set; }

    }
}
