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
        [Display(Name = "���")]
        [Key]
        [StringLength(50)]
        public string cid { get; set; }

        [Display(Name = "�γ���")]
        [Required]
        [StringLength(50)]
        public string cname { get; set; }

        [Display(Name = "������ʦ����")]
        [Required]
        [StringLength(50)]
        public string tid { get; set; }

        [Display(Name = "����רҵ")]
        [Required]
        [StringLength(50)]
        public string cmajor { get; set; }
    }
}
