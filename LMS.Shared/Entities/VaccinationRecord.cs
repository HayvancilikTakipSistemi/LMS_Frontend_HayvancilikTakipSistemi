using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("VaccinationRecords")]
    public class VaccinationRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VaccinationRecordID { get; set; }

        public int AnimalID { get; set; }
        public int? VeterinarianID { get; set; }
        public int VaccineID { get; set; }

        [Required]
        public DateTime ApplicationDate { get; set; }

        public DateTime? NextDate { get; set; }

        [MaxLength(200)]
        public string? Notes { get; set; }

        [ForeignKey("AnimalID")]
        public virtual Animal? Animal { get; set; }

        [ForeignKey("VeterinarianID")]
        public virtual Veterinarian? Veterinarian { get; set; }

        [ForeignKey("VaccineID")]
        public virtual Vaccine? Vaccine { get; set; }
    }
}
