using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("AsiKaydi")]
    public class AsiKaydi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AsiKayitID { get; set; }
        
        public int HayvanID { get; set; }
        public int? VeterinerID { get; set; }
        public int AsiID { get; set; }
        
        [Required]
        public DateTime UygulamaTarihi { get; set; }
        
        public DateTime? SonrakiTarih { get; set; } // Hesaplanabilir ama saklamak pratik
        
        [MaxLength(200)]
        public string? Notlar { get; set; }

        public virtual Hayvan? Hayvan { get; set; }
        public virtual Veteriner? Veteriner { get; set; }
        public virtual Asi? Asi { get; set; }
    }
}