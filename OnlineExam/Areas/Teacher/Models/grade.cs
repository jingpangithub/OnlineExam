namespace OnlineExam.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("grade")]
    public partial class grade
    {
        [Display(Name = "���")]
        [Key]
        [StringLength(50)]
        public string gid { get; set; }

        [Display(Name = "����ѧ��")]
        [Required]
        [StringLength(50)]
        public string sid { get; set; }

        [Display(Name = "���Ա��")]
        [Required]
        [StringLength(50)]
        public string eid { get; set; }

        [Display(Name = "�ɼ�")]
        public int? gra { get; set; }
    }
}
