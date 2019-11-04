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
        [Display(Name = "��ʦ����")]
        [Key]
        [StringLength(50)]
        public string tid { get; set; }

        [Display(Name = "����")]
        [Required]
        [StringLength(50)]
        public string tname { get; set; }

        [Display(Name = "�Ա�")]
        [StringLength(50)]
        public string tsex { get; set; }

        [Display(Name = "����")]
        [StringLength(50)]
        public string tage { get; set; }

        [Display(Name = "ϵ��")]
        [StringLength(50)]
        public string tdepartment { get; set; }

        [Display(Name = "����")]
        [Required]
        [StringLength(50)]
        public string tpassword { get; set; }
    }
}
