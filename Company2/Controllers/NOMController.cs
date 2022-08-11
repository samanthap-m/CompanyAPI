﻿using AutoMapper;
using Company2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NOMController : Controller
    {
        private readonly IMapper _mapper;
        private readonly CompanyContext _context = new CompanyContext();
        public NOMController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var employees = _context.Employees.Include(_ => _.Department).ToList();
            List<Employee2> result = _mapper.Map<List<Employee>, List<Employee2>>(employees);
            return Ok(result);
        }
    }
}
