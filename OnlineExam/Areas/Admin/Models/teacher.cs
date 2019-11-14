namespace OnlineExam.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("teacher")]
    public partial class teacher
    {
        [Display(Name = "教师工号")]
        [Key]
        [StringLength(50)]
        public string tid { get; set; }

        [Display(Name = "姓名")]
        [Required]
        [StringLength(50)]
        public string tname { get; set; }

        [Display(Name = "性别")]
        [StringLength(50)]
        public string tsex { get; set; }

        [Display(Name = "年龄")]
        [StringLength(50)]
        public string tage { get; set; }

        [Display(Name = "系别")]
        [StringLength(50)]
        public string tdepartment { get; set; }

        [Display(Name = "密码")]
        [Required]
        [StringLength(50)]
        public string tpassword { get; set; }
    }
}
