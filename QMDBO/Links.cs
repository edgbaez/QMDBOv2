using System;
using System.Collections.Generic;

namespace QMDBO
{
    public partial class Links
    {
        public int id { get; set; }
        public int category_id { get; set; }
        public string name { get; set; }
        public string host { get; set; }
        public string port { get; set; }
        public string servicename { get; set; }
        public string user { get; set; }
        public string pass { get; set; }


        public virtual Category Category { get; set; }
    }
}
