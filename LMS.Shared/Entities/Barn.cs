using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Barn")]
    public class Barn
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BarnID { get; set; }

        public int FarmerID { get; set; }

        [Required]
        [MaxLength(100)]
        public string BarnName { get; set; } = string.Empty;

        public int? Capacity { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        // Navigation Properties
        public virtual Farmer? Farmer { get; set; }
        public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
    }
}
