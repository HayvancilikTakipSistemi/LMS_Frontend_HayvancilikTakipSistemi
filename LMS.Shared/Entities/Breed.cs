using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Breed")]
    public class Breed
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BreedID { get; set; }

        public int AnimalTypeID { get; set; }

        [Required]
        [MaxLength(50)]
        public string BreedName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(6,2)")]
        public decimal? MilkCapacityLiters { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        // Navigation Properties
        public virtual AnimalType? AnimalType { get; set; }
        public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
    }
}
