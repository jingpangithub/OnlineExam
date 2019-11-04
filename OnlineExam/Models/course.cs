namespace OnlineExam.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("course")]
    public partial class course
    {
        [Display(Name = "编号")]
        [Key]
        [StringLength(50)]
        public string cid { get; set; }

        [Display(Name = "课程名")]
        [Required]
        [StringLength(50)]
        public string cname { get; set; }

        [Display(Name = "所属教师工号")]
        [Required]
        [StringLength(50)]
        public string tid { get; set; }

        [Display(Name = "所属专业")]
        [Required]
        [StringLength(50)]
        public string cmajor { get; set; }
    }
}
