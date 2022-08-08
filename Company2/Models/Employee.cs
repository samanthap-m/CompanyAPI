using System.ComponentModel.DataAnnotations;

namespace Company2.Models
{
    public class Employee 
    {
        [Key]
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string? PhotoFileName { get; set; }
    }
}
