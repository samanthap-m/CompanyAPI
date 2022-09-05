using System.ComponentModel.DataAnnotations;
namespace Company2.Models
{
    public class EmployeeModel
    {
        [Key]
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string? PhotoFileName { get; set; }
        public string? DepartmentName { get; set; }
    }
}