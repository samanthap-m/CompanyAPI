using AutoMapper;
using Company2.Models;

namespace Company2
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, Employee2>();
            CreateMap<Department, Department2>();
        }
    }
}
