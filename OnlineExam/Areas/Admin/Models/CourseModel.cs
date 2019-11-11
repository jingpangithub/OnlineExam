namespace OnlineExam.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CourseModel : DbContext
    {
        public CourseModel()
            : base("name=CourseModel")
        {
        }

        public virtual DbSet<course> course { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
