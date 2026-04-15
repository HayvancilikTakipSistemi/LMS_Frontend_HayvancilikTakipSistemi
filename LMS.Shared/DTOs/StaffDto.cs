using System;

namespace LMS.Shared.DTOs
{
    public class StaffDto
    {
        public int StaffID { get; set; }
        public int FarmerID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Role { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? HireDate { get; set; }
        public string? Phone { get; set; }
    }
}
