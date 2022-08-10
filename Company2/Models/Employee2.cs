using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company2.Models
{
    public class Employee2
    {
        [Key]
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }        
        public virtual int? DepartmentId { get; set; }
    }
}
