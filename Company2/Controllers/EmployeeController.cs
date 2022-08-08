using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Company2.Models;

namespace Company2.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public JsonResult Get()
        {
            using (var context = new CompanyContext())
            {
                var employee = context.Employees.Join(context.Departments, p => p.DepartmentId, e => e.DepartmentId, (p, e) => new
                {
                    p.EmployeeId,
                    p.EmployeeName,
                    p.DateOfJoining,
                    e.DepartmentName,
                    p.PhotoFileName,
                })
                   .Select(r => new
                   {
                       EmployeeId = r.EmployeeId,
                       EmployeeName = r.EmployeeName,
                       DateOfJoining = r.DateOfJoining,
                       DepartmentName = r.DepartmentName,
                       PhotoFileName = r.PhotoFileName
                   }).ToList();
                //Return all Employees
                return new JsonResult(employee);

                //Return Employee with employeeid specified
                //return new JsonResult(context.Employees.Where(emp => emp.EmployeeId == employeeId).ToList());

            }
        }

        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            //Add Employee
            using (var context = new CompanyContext())
            {

                context.Employees.Add(emp);

                context.SaveChanges();

                var employee = context.Employees.Join(context.Departments, p => p.DepartmentId, e => e.DepartmentId, (p, e) => new
                {
                    p.EmployeeId,
                    p.EmployeeName,
                    p.DateOfJoining,
                    e.DepartmentName,
                    p.PhotoFileName,
                })
                   .Select(r => new
                   {
                       EmployeeId = r.EmployeeId,
                       EmployeeName = r.EmployeeName,
                       DateOfJoining = r.DateOfJoining,
                       DepartmentName = r.DepartmentName,
                       PhotoFileName = r.PhotoFileName
                   }).ToList();
                //Return all Employees
                return new JsonResult(employee);
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

                var employee = context.Employees.Join(context.Departments, p => p.DepartmentId, e => e.DepartmentId, (p, e) => new
                {
                    p.EmployeeId,
                    p.EmployeeName,
                    p.DateOfJoining,
                    e.DepartmentName,
                    p.PhotoFileName,
                })
                   .Select(r => new
                   {
                       EmployeeId = r.EmployeeId,
                       EmployeeName = r.EmployeeName,
                       DateOfJoining = r.DateOfJoining,
                       DepartmentName = r.DepartmentName,
                       PhotoFileName = r.PhotoFileName
                   }).ToList();
                //Return all Employees
                return new JsonResult(employee);
            }
        }

        [HttpDelete("{employeeId}")]
        public JsonResult Delete(int employeeId)
        {
            using (var context = new CompanyContext())
            {
                //Remove employee
                Employee emp = context.Employees.Where(emp => emp.EmployeeId == employeeId).FirstOrDefault();

                context.Employees.Remove(emp);
                context.SaveChanges();

                var employee = context.Employees.Join(context.Departments, p => p.DepartmentId, e => e.DepartmentId, (p, e) => new
                {
                    p.EmployeeId,
                    p.EmployeeName,
                    p.DateOfJoining,
                    e.DepartmentName,
                    p.PhotoFileName,
                })
                   .Select(r => new
                   {
                       EmployeeId = r.EmployeeId,
                       EmployeeName = r.EmployeeName,
                       DateOfJoining = r.DateOfJoining,
                       DepartmentName = r.DepartmentName,
                       PhotoFileName = r.PhotoFileName
                   }).ToList();
                //Return all Employees
                return new JsonResult(employee);
            }
        }
    }
}