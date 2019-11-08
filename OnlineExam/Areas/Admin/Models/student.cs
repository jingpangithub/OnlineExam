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
        [Display(Name = "学号")]
        [Key]
        [StringLength(50)]
        public string sid { get; set; }

        [Display(Name = "姓名")]
        [Required]
        [StringLength(50)]
        public string sname { get; set; }

        [Display(Name = "性别")]
        [StringLength(50)]
        public string ssex { get; set; }

        [Display(Name = "年龄")]
        [StringLength(50)]
        public string sage { get; set; }

        [Display(Name = "专业")]
        [Required]
        [StringLength(50)]
        public string smajor { get; set; }

        [Display(Name = "密码")]
        [Required]
        [StringLength(50)]
        public string spassword { get; set; }
    }
}
