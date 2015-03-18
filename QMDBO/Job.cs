using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QMDBO
{
    public partial class Job
    {
        public Job()
        {
            Result = new List<Result>();
        }

        public int JobId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Result> Result { get; set; }
    }
}
