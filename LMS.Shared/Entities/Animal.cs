using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Animal")]
    public class Animal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnimalID { get; set; }

        public int FarmerID { get; set; }

        public int? BarnID { get; set; }

        public int? BreedID { get; set; }

        public int AnimalStatusID { get; set; } = 1;

        [Required]
        [MaxLength(20)]
        public string KupeNo { get; set; } = string.Empty;

        [Required]
        [MaxLength(1)]
        public string Gender { get; set; } = string.Empty; // E / D

        public DateTime? BirthDate { get; set; }

        public int? MotherAnimalID { get; set; }

        // Navigation Properties
        public virtual Farmer? Farmer { get; set; }
        public virtual Barn? Barn { get; set; }
        public virtual Breed? Breed { get; set; }
        public virtual AnimalStatus? AnimalStatus { get; set; }
        public virtual Animal? MotherAnimal { get; set; }
        
        public virtual ICollection<Animal> OffspringAnimals { get; set; } = new List<Animal>();
    }
}
