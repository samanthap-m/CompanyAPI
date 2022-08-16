using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Company2.Models;
using AutoMapper.QueryableExtensions;

namespace Company2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        [HttpGet("{di}")]
        public List<EmployeeModel> Get(int di)
        {
            var c = new MapperConfiguration(cfg => cfg.CreateProjection<Employee, EmployeeModel>()
                                                    .ForMember(dto => dto.DepartmentName,
                                            conf => conf.MapFrom(ol => ol.Department.DepartmentId)));

            using(var _context=new CompanyContext())
            {
                return _context.Employees.Where(ol => ol.DepartmentId == di).ProjectTo<EmployeeModel>(c).ToList();
            }
        }
    }
}
