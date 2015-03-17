using System;
using System.Collections.Generic;

namespace QMDBO
{
    public partial class Link
    {
        public int LinkId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Servicename { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }


        public virtual Category Category { get; set; }
    }
}
