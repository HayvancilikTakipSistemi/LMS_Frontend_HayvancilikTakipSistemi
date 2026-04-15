using System;

namespace LMS.Shared.DTOs
{
    public class TransactionDto
    {
        public int TransactionID { get; set; }
        public int CustomerID { get; set; }
        public int? SaleID { get; set; }
        public int TransactionTypeID { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
    }
}
