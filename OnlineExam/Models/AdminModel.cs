namespace OnlineExam.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AdminModel : DbContext
    {
        public AdminModel()
            : base("name=AdminModel")
        {
        }

        public virtual DbSet<admin> admin { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
