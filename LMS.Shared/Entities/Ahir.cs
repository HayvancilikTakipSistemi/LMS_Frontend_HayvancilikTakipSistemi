using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Ahir")]
    public class Ahir
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AhirID { get; set; }
        
        public int CiftciID { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string AhirAdi { get; set; } = string.Empty;
        
        public int? Kapasite { get; set; } // Max hayvan sayısı
        
        [MaxLength(200)]
        public string? Adres { get; set; }

        public virtual Ciftci? Ciftci { get; set; }
        public virtual ICollection<Hayvan> Hayvanlar { get; set; } = new List<Hayvan>();
    }
}