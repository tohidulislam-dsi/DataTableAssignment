using AutoMapper;
using DataTableAssignment.Web.Models.Domain;
using DataTableAssignment.Web.Models.Dto;   

namespace DataTableAssignment.Web.Mapping
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
