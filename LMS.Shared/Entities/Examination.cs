using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Examinations")]
    public class Examination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExaminationID { get; set; }

        public int AnimalID { get; set; }
        public int VeterinarianID { get; set; }

        [Required]
        public DateTime ExaminationDate { get; set; }

        [MaxLength(300)]
        public string? Diagnosis { get; set; }

        [MaxLength(300)]
        public string? Treatment { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        [ForeignKey("AnimalID")]
        public virtual Animal? Animal { get; set; }

        [ForeignKey("VeterinarianID")]
        public virtual Veterinarian? Veterinarian { get; set; }

        [InverseProperty("Examination")]
        public virtual ICollection<ExaminationDrug> ExaminationDrugs { get; set; } = new List<ExaminationDrug>();
    }
}
