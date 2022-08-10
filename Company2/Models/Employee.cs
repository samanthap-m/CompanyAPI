using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company2.Models
{
    public class Employee 
    {
        [Key]
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string? PhotoFileName { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
