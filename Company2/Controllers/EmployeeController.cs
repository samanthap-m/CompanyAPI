using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Company2.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Company2.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class EmployeeController : ControllerBase
    {
        CompanyContext _context = new CompanyContext();
        List<Employee> employees = new List<Employee>();

        public List<Employee> Getemployees(CompanyContext _context)
        {
            //employees = _context.Employees.Include(m => m.DepartmentId).ToList();
            employees = _context.Employees.ToList();

            return employees;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var employee = _context.Employees.Join(_context.Departments, p => p.DepartmentId, e => e.DepartmentId, (p, e) => new
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

        [HttpGet]
        [MapToApiVersion("2")]
        public JsonResult Get2()
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

        [HttpGet]
        [MapToApiVersion("3")]
        public JsonResult Get3()
        {
            var c = new MapperConfiguration(cfg => cfg.CreateProjection<Employee, EmployeeModel>()
                                                      .ForMember(dto => dto.DepartmentName, conf =>
                                                  conf.MapFrom(ol => ol.Department.DepartmentName)));

            return new JsonResult(_context.Employees.ProjectTo<EmployeeModel>(c).ToList());
        }

        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            //Add Employee
            _context.Employees.Add(emp);

            _context.SaveChanges();

            var employee = _context.Employees.Join(_context.Departments, p => p.DepartmentId, e => e.DepartmentId, (p, e) => new
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

        [HttpPost]
        [MapToApiVersion("2")]
        public JsonResult Post2(Employee emp2)
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
            return (this.Get2());
        }

        [HttpPost]
        [MapToApiVersion("3")]
        public JsonResult Post3(Employee emp2)
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
            return (this.Get3());
        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            //Update Employee
            Employee? emp1 = _context.Employees.Where(emp1 => emp1.EmployeeId == emp.EmployeeId).FirstOrDefault();
            emp1.EmployeeName = emp.EmployeeName;
            emp1.PhotoFileName = emp.PhotoFileName;
            emp1.DateOfJoining = emp.DateOfJoining;
            emp1.DepartmentId = emp.DepartmentId;

            _context.SaveChanges();

            var employee = _context.Employees.Join(_context.Departments, p => p.DepartmentId, e => e.DepartmentId, (p, e) => new
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

        [HttpPut]
        [MapToApiVersion("2")]
        public JsonResult Put2(Employee emp)
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
            return (this.Get2());
        }

        [HttpPut]
        [MapToApiVersion("3")]
        public JsonResult Put3(Employee emp)
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
            return (this.Get3());
        }

        [HttpDelete("{employeeId}")]
        public JsonResult Delete(int employeeId)
        {
            //Remove employee
            Employee? emp = _context.Employees.Where(emp => emp.EmployeeId == employeeId).FirstOrDefault();

            _context.Employees.Remove(emp);
            _context.SaveChanges();

            var employee = _context.Employees.Join(_context.Departments, p => p.DepartmentId, e => e.DepartmentId, (p, e) => new
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

        [HttpDelete("{employeeId}")]
        [MapToApiVersion("2")]
        public JsonResult Delete2(int employeeId)
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
            return (this.Get2());
        }

        [HttpDelete("{employeeId}")]
        [MapToApiVersion("3")]
        public JsonResult Delete3(int employeeId)
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
            return (this.Get3());
        }
    }
}