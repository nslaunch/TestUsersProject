using AutoMapper;
using TestUsersProject.Users.Dto;

namespace TestUsersProject.Web.Helpers
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
        }
    }
}
