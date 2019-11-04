namespace OnlineExam.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ExamModel : DbContext
    {
        public ExamModel()
            : base("name=ExamModel")
        {
        }

        public virtual DbSet<exam> exam { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
