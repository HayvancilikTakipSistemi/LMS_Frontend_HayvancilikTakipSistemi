using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Irk")]
    public class Irk
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IrkID { get; set; }
        
        public int HayvanTuruID { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string IrkAdi { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(6,2)")]
        public decimal? SutKapasitesi { get; set; } // Günlük litre kapasitesi
        
        [MaxLength(200)]
        public string? Aciklama { get; set; }

        public virtual HayvanTuru? HayvanTuru { get; set; }
        public virtual ICollection<Hayvan> Hayvanlar { get; set; } = new List<Hayvan>();
    }
}