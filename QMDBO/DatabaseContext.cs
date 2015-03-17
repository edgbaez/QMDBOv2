using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace QMDBO
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Result> Results { get; set; }
    }
}
