using Microsoft.AspNetCore.Mvc;
using System.Data;
using Company2.Models;
using Microsoft.EntityFrameworkCore;

namespace Company2.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("[controller]")]
    public class Employee2Controller : ControllerBase
    {
        CompanyContext _context = new CompanyContext();
        List<Employee> employees = new List<Employee>();

        public List<Employee> Getemployees(CompanyContext _context)
        {
            employees = _context.Employees.Include(m => m.Department).ToList();

            return employees;
        }

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                employees = this.Getemployees(_context);
            }
            catch (Exception ex)
            {
                throw;
            }  
            //Return all Employees
            return new JsonResult(employees);
        }

         [HttpPost]
         public JsonResult Post(Employee emp2)
         {
            try
            {
                //Add Employee
                _context.Employees.Add(emp2);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
            //Return all Employees
            return (this.Get());
        }

         [HttpPut]
         public JsonResult Put(Employee emp)
         {
            try
            {
                //Update Employee
                _context.Employees.Update(emp);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            //Return all Employees
            return (this.Get());
        }

         [HttpDelete("{employeeId}")]
         public JsonResult Delete(int employeeId)
         {
            try
            {
                // Remove employee
                Employee? emp2 = _context.Employees.Where(emp => emp.EmployeeId == employeeId).FirstOrDefault();

                _context.Employees.Remove(emp2);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
            
            //Return all Employees
            return (this.Get());
         }
    }
}