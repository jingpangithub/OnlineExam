namespace OnlineExam.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("admin")]
    public partial class admin
    {
        [Display(Name = "±‡∫≈")]
        [Key]
        [StringLength(50)]
        public string aid { get; set; }

        [Display(Name = "–’√˚")]
        [Required]
        [StringLength(50)]
        public string aname { get; set; }

        [Display(Name = "√‹¬Î")]
        [Required]
        [StringLength(50)]
        public string apassword { get; set; }
    }
}
