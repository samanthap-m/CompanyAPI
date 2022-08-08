using Microsoft.AspNetCore.Mvc;
using System.Data;
using Company2.Models;

namespace Company2.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("[controller]")]
    public class Employee2Controller : ControllerBase
    {
        public List<dynamic> Getemployees(CompanyContext _context)
        {
            var employees = (from emp in _context.Employees
                             join jd in _context.Departments on emp.DepartmentId equals jd.DepartmentId
                             select new
                             {
                                 emp.EmployeeId,
                                 emp.EmployeeName,
                                 emp.DateOfJoining,
                                 jd.DepartmentName,
                                 emp.PhotoFileName
                             }).ToList();
            return employees.ToList<dynamic>();
        }

        [HttpGet]
        public JsonResult Get()
        {
            using (var context = new CompanyContext())
            {
               var emps = this.Getemployees(context);
            // var employees = context.Employees.Join(ToList();
            //Return all Employees
            return new JsonResult(emps);
             }
         }

         [HttpPost]
         public JsonResult Post(Employee emp2)
         {
             //Add Employee
             using (var context = new CompanyContext())
             {

                 context.Employees.Add(emp2);

                 context.SaveChanges();

                var employees = this.Getemployees(context);

                //Return all Employees
                return new JsonResult(employees);
             }
         }

         [HttpPut]
         public JsonResult Put(Employee emp)
         {
             //Update Employee
             using (var context = new CompanyContext())
             {
                 Employee emp1 = context.Employees.Where(emp1 => emp1.EmployeeId == emp.EmployeeId).FirstOrDefault();
                 emp1.EmployeeName = emp.EmployeeName;
                 emp1.PhotoFileName = emp.PhotoFileName;
                 emp1.DateOfJoining = emp.DateOfJoining;
                 emp1.DepartmentId = emp.DepartmentId;

                 context.SaveChanges();

                var employees = this.Getemployees(context);
                //Return all Employees
                return new JsonResult(employees);
             }
         }

         [HttpDelete("{employeeId}")]
         public JsonResult Delete(int employeeId)
         {
             using (var context = new CompanyContext())
             {
                 //Remove employee
                 Employee emp2 = context.Employees.Where(emp => emp.EmployeeId == employeeId).FirstOrDefault();

                 context.Employees.Remove(emp2);
                 context.SaveChanges();

                var employees = this.Getemployees(context);
                //Return all Employees
                return new JsonResult(employees);
             }
         }
    }
}



