using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs
{
    public class AnimalDto
    {
        public int AnimalID { get; set; }

        public int FarmerID { get; set; }

        public int? BarnID { get; set; }

        public int? BreedID { get; set; }

        public int AnimalStatusID { get; set; }

        [Required]
        [MaxLength(20)]
        public string KupeNo { get; set; } = string.Empty;

        [Required]
        [MaxLength(1)]
        public string Gender { get; set; } = string.Empty;

        public DateTime? BirthDate { get; set; }

        public int? MotherAnimalID { get; set; }

        public string? BreedName { get; set; }

        public string? BarnName { get; set; }

        public string? StatusName { get; set; }

        public string? FarmerName { get; set; }
    }
}
