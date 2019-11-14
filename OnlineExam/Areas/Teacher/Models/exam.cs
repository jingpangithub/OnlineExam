namespace OnlineExam.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam")]
    public partial class exam
    {
        [Display(Name = "编号")]
        [Key]
        [StringLength(50)]
        public string eid { get; set; }

        [Display(Name = "所属教师编号")]
        [Required]
        [StringLength(50)]
        public string tid { get; set; }

        [Display(Name = "所属专业")]
        [Required]
        [StringLength(50)]
        public string emajor { get; set; }

        [Display(Name = "试题内容")]
        [StringLength(50)]
        public string etest { get; set; }

        [Display(Name = "试题答案")]
        [StringLength(50)]
        public string eanswer { get; set; }

        [Display(Name = "总分")]
        public int? egrade { get; set; }
    }
}
