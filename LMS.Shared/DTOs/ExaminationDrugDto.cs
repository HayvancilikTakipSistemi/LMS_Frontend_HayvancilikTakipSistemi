using System;
using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs
{
    public class ExaminationDrugDto
    {
        public int ExaminationID { get; set; }
        public int DrugID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Dosage { get; set; } = string.Empty;

        public DateTime? ExpiryDate { get; set; }
    }
}
