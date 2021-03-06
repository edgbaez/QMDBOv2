using System;
using System.Collections.Generic;


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
        public string Code { get; set; }
        public int TypeExecute { get; set; }
        public string ObjName { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Result> Result { get; set; }
    }
}
