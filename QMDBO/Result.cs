using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMDBO
{
    public partial class Result
    {
        public int ResultId { get; set; }
        public int JobId { get; set; }
        public int LinkId { get; set; }
        public string Content { get; set; }
        public string Object_type { get; set; }
        public string Object_status { get; set; }
        public string Last_ddl_time { get; set; }

        public virtual Link Link { get; set; }
        public virtual Job Job { get; set; }

    }
}
