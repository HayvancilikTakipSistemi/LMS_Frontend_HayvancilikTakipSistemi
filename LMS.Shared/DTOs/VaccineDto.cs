using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs
{
    public class VaccineDto
    {
        public int VaccineID { get; set; }

        [Required]
        [MaxLength(100)]
        public string VaccineName { get; set; } = string.Empty;

        public int? PeriodDays { get; set; }
    }
}
