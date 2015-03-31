using System;
using System.Collections.Generic;

namespace QMDBO
{
    public partial class Value
    {
        public int ValueId { get; set; }
        public int LinkId { get; set; }
        public int KeyId { get; set; }
        public string KeyValue { get; set; }

        public virtual Key Key { get; set; }
    }
}
