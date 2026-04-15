using System;
using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs
{
    public class ExaminationDto
    {
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

        public List<ExaminationDrugDto> PrescribedDrugs { get; set; } = new List<ExaminationDrugDto>();
    }
}
