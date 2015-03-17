using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMDBO
{
    public partial class Results
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int LinkId { get; set; }
        public string Content { get; set; }

        public virtual Category Category { get; set; }
        public virtual Jobs Jobs { get; set; }

    }
}
