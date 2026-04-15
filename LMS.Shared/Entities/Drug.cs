using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Drugs")]
    public class Drug
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DrugID { get; set; }

        [Required]
        [MaxLength(100)]
        public string DrugName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Unit { get; set; } = string.Empty; // e.g. ml, mg, tablet

        [MaxLength(200)]
        public string? Description { get; set; }

        [InverseProperty("Drug")]
        public virtual ICollection<ExaminationDrug> ExaminationDrugs { get; set; } = new List<ExaminationDrug>();
    }
}
