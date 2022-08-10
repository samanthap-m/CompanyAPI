using AutoMapper;
using Company2.Models;

namespace Company2
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, Employee2>()
                .ForMember(dest => dest.EmployeeId,
                           opt => opt.MapFrom(src => src.EmployeeId));
            CreateMap<Employee2, Employee>();
        }
    }
}
