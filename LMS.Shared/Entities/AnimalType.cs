using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("AnimalType")]
    public class AnimalType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnimalTypeID { get; set; }

        [Required]
        [MaxLength(50)]
        public string TypeName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }

        // Navigation Properties
        public virtual ICollection<Breed> Breeds { get; set; } = new List<Breed>();
    }
}
