using System;
using System.Collections.Generic;

namespace QMDBO
{
    public partial class Category
    {
        public Category()
        {
            this.Links = new ObservableListSource<Links>();
        }

        public long id { get; set; }
        public string name { get; set; }

        public virtual ObservableListSource<Links> Links { get; set; }
    }
}
