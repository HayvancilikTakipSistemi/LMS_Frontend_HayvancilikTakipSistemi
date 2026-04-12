using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Hayvan")]
    public class Hayvan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HayvanID { get; set; }
        public int CiftciID { get; set; }
        public int? AhirID { get; set; }
        public int? IrkID { get; set; }
        public int HayvanDurumID { get; set; } = 1;
        
        [Required]
        [MaxLength(20)]
        public string KupeNo { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(1)]
        public string Cinsiyet { get; set; } = string.Empty; // 'E' or 'D'
        
        public DateTime? DogumTarihi { get; set; }
        public int? AnneID { get; set; }

        public Ciftci? Ciftci { get; set; }
    }
}
