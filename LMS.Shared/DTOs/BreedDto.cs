using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs
{
    public class BreedDto
    {
        public int BreedID { get; set; }

        public int AnimalTypeID { get; set; }

        [Required]
        [MaxLength(50)]
        public string BreedName { get; set; } = string.Empty;

        public decimal? MilkCapacityLiters { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public string? AnimalTypeName { get; set; }
    }
}
