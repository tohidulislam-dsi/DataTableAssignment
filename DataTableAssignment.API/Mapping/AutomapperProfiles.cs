using AutoMapper;
using DataTableAssignment.API.Models.Domain;
using DataTableAssignment.API.Models.Dto;   

namespace DataTableAssignment.API.Mapping
{
    public class AutomapperProfiles:Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Employee,EmployeeDto>().ReverseMap();
        }
    }
}
