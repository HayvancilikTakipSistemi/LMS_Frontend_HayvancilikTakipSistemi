using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LMS.Shared.Enums;

namespace LMS.Shared.Entities
{
    [Table("Kullanici")]
    public class Kullanici
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KullaniciID { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string KullaniciAdi { get; set; } = string.Empty;
        
        [Required]
        public string SifreHash { get; set; } = string.Empty;
        
        public Role Rol { get; set; }
        public bool AktifMi { get; set; } = true;
        
        public int? BagliCiftciID { get; set; }
        public Ciftci? BagliCiftci { get; set; }
    }
}
