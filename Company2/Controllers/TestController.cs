using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Company2.Models;

namespace Company2.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly IMapper _mapper;
        private readonly CompanyContext _context = new CompanyContext(); 
        public TestController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet]
        
        public IActionResult GetFirstGadgetWithMapping()
        {
            var employees = _context.Employees.FirstOrDefault();
            var result = _mapper.Map<Employee, Employee2>(employees);
            return Ok(result);
        }
    }
}
