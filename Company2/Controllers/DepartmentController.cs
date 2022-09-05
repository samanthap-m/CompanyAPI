using Microsoft.AspNetCore.Mvc;
using System.Data;
using Company2.Models;

namespace Company2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Department> Get()
        {
            using (var context = new CompanyContext())
            {
                //Return all Departments
                return context.Departments.ToList();
            }
        }

        [HttpPost]
        public IEnumerable<Department> Post(Department dep)
        {
            using (var context = new CompanyContext())
            {
                Add Maintainance department
                dep.DepartmentName = "Maintainance";

                context.Departments.Add(dep);

                context.SaveChanges();

                return context.Departments.ToList();
            }
        }

        [HttpPut]
        public IEnumerable<Department> Put(Department dep)
        {
            using (var context = new CompanyContext())
            {
                //Update Maintainance department
                Department? dep1 = context.Departments.Where(dep1 => dep1.DepartmentId == dep.DepartmentId).FirstOrDefault();
                dep1.DepartmentName = dep.DepartmentName;

                context.SaveChanges();

                return context.Departments.ToList();
            }
        }

        [HttpDelete("{departmentId}")]
        public IEnumerable<Department> Delete(int departmentId)
        {
            using (var context = new CompanyContext())
            {
                //Remove department
                Department? dep = context.Departments.Where(dep => dep.DepartmentId == departmentId).FirstOrDefault();

                context.Departments.Remove(dep);
                context.SaveChanges();

                return context.Departments.ToList();
            }
        }
    }
}