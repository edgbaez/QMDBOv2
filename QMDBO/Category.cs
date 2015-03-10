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

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ObservableListSource<Links> Links { get; set; }
    }
}
