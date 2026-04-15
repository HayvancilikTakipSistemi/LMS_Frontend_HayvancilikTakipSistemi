using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Asi")]
    public class Asi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AsiID { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string AsiAdi { get; set; } = string.Empty;
        
        public int? PeriyotGun { get; set; } // Tekrar periyodu (gün)

        public virtual ICollection<AsiKaydi> AsiKayitlari { get; set; } = new List<AsiKaydi>();
    }
}