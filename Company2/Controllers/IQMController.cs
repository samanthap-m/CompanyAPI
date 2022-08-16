using AutoMapper;
using AutoMapper.QueryableExtensions;
using Company2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class IQMController : Controller
    {
        private readonly IMapper _mapper;
        private readonly CompanyContext _context = new CompanyContext();
        public IQMController(IMapper mapper)
        {
            _mapper = mapper;
        }

/*        [HttpGet]
        public IActionResult Get()
        {
            var employees = _context.Employees.Include(_ => _.Department).ProjectTo<Employee2>(_mapper.ConfigurationProvider).ToList();
            return Ok(employees);
        }*/
    }
}



