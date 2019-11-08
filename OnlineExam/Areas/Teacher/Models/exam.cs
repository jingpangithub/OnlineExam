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
        [Display(Name = "���")]
        [Key]
        [StringLength(50)]
        public string eid { get; set; }

        [Display(Name = "������ʦ���")]
        [Required]
        [StringLength(50)]
        public string tid { get; set; }

        [Display(Name = "����רҵ")]
        [Required]
        [StringLength(50)]
        public string emajor { get; set; }

        [Display(Name = "��������")]
        [StringLength(50)]
        public string etest { get; set; }

        [Display(Name = "�����")]
        [StringLength(50)]
        public string eanswer { get; set; }

        [Display(Name = "�ܷ�")]
        public int? egrade { get; set; }
    }
}
