using AutoMapper;
using DataTableAssignment.Web.Models.Domain;
using DataTableAssignment.Web.Models.Dto;
using DataTableAssignment.Web.Models.Response;

namespace DataTableAssignment.Web.Mapping
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<EmployeeFilterResultDto<Employee>, EmployeeFilterResultDto<EmployeeDto>>()
                .ForMember(dest => dest.data, opt => opt.MapFrom(src => src.data));
            CreateMap<EmployeeFilterResultDto<EmployeeDto>, EmployeeFilterResponseModel>()
                .ForMember(dest => dest.data, opt => opt.MapFrom(src => src.data))
                .ForMember(dest => dest.recordsTotal, opt => opt.MapFrom(src => src.recordsTotal))
                .ForMember(dest => dest.recordsFiltered, opt => opt.MapFrom(src => src.recordsFiltered));



        }
    }
}
