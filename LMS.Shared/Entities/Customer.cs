using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        
        [MaxLength(15)]
        public string? Phone { get; set; }
        
        [MaxLength(250)]
        public string? Address { get; set; }
        
        [MaxLength(50)]
        public string? CustomerType { get; set; } // Individual, Corporate
        
        [MaxLength(300)]
        public string? Notes { get; set; }

        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public virtual ICollection<MoneyTransaction> MoneyTransactions { get; set; } = new List<MoneyTransaction>();
    }
}
