namespace OnlineExam.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("student")]
    public partial class student
    {
        [Display(Name = "ѧ��")]
        [Key]
        [StringLength(50)]
        public string sid { get; set; }

        [Display(Name = "����")]
        [Required]
        [StringLength(50)]
        public string sname { get; set; }

        [Display(Name = "�Ա�")]
        [StringLength(50)]
        public string ssex { get; set; }

        [Display(Name = "����")]
        [StringLength(50)]
        public string sage { get; set; }

        [Display(Name = "רҵ")]
        [Required]
        [StringLength(50)]
        public string smajor { get; set; }

        [Display(Name = "����")]
        [Required]
        [StringLength(50)]
        public string spassword { get; set; }
    }
}
