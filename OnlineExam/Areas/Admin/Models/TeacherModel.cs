namespace OnlineExam.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TeacherModel : DbContext
    {
        public TeacherModel()
            : base("name=TeacherModel")
        {
        }

        public virtual DbSet<teacher> teacher { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
