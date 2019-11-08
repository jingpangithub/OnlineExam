namespace OnlineExam.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GradeModel : DbContext
    {
        public GradeModel()
            : base("name=GradeModel")
        {
        }

        public virtual DbSet<grade> grade { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
