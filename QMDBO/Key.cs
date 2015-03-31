using System;
using System.Collections.Generic;

namespace QMDBO
{
    public partial class Key
    {
        public int KeyId { get; set; }
        public int JobId { get; set; }
        public string Name { get; set; }
        public string DbType { get; set; }
        public string Size { get; set; }
        public string InValue { get; set; }
        public int Type { get; set; }

        public virtual Job Job { get; set; }
        public virtual Link Link { get; set; }
        
    }
}
