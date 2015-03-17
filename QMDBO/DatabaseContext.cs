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
        public virtual DbSet<Links> Links { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<Results> Results { get; set; }
    }
}
