using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("ExaminationDrugs")]
    public class ExaminationDrug
    {
        public int ExaminationID { get; set; }
        public int DrugID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Dosage { get; set; } = string.Empty;

        public DateTime? ExpiryDate { get; set; }

        [ForeignKey("ExaminationID")]
        public virtual Examination? Examination { get; set; }

        [ForeignKey("DrugID")]
        public virtual Drug? Drug { get; set; }
    }
}
