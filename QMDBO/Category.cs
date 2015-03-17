using System;
using System.Collections.Generic;

namespace QMDBO
{
    public partial class Category
    {
        public Category()
        {
            this.Link = new ObservableListSource<Link>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ObservableListSource<Link> Link { get; set; }
    }
}
