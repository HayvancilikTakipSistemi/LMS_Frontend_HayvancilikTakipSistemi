using System;
using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs
{
    public class VaccinationRecordDto
    {
        public int VaccinationRecordID { get; set; }
        public int AnimalID { get; set; }
        public int? VeterinarianID { get; set; }
        public int VaccineID { get; set; }

        [Required]
        public DateTime ApplicationDate { get; set; }

        public DateTime? NextDate { get; set; }

        [MaxLength(200)]
        public string? Notes { get; set; }
    }
}
