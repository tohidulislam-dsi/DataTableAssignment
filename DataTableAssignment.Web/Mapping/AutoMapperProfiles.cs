using AutoMapper;
using DataTableAssignment.Web.Models.Domain;
using DataTableAssignment.Web.Models.Dto;
using DataTableAssignment.Web.Models.Response;
using DataTableAssignment.Web.Models.ViewModel;

namespace DataTableAssignment.Web.Mapping
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<EmployeeDto, EmployeeViewModel>().ReverseMap();
            CreateMap<EmployeeWithTotalFilteredRecords, Employee>().ReverseMap();
            CreateMap<EmployeeFilterResultDto<Employee>, EmployeeFilterResultDto<EmployeeDto>>()
                .ForMember(dest => dest.data, opt => opt.MapFrom(src => src.data));
            CreateMap<EmployeeFilterResultDto<EmployeeWithTotalFilteredRecords>, EmployeeFilterResultDto<EmployeeDto>>()
                .ForMember(dest => dest.data, opt => opt.MapFrom(src => src.data));
            CreateMap<EmployeeFilterResultDto<EmployeeDto>, EmployeeFilterResultDto<EmployeeViewModel>>()
                .ForMember(dest => dest.data, opt => opt.MapFrom(src => src.data));
            CreateMap<EmployeeFilterResultDto<EmployeeDto>, EmployeeFilterResponseModel>()
                .ForMember(dest => dest.data, opt => opt.MapFrom(src => src.data))
                .ForMember(dest => dest.recordsTotal, opt => opt.MapFrom(src => src.recordsTotal))
                .ForMember(dest => dest.recordsFiltered, opt => opt.MapFrom(src => src.recordsFiltered));


        }
    }
}
