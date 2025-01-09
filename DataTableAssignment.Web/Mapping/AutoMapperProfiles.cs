using AutoMapper;
using DataTableAssignment.Web.Models.Entities;
using DataTableAssignment.Web.Models.Dto;
using DataTableAssignment.Web.Models.Response;
using DataTableAssignment.Web.Models.ViewModel;
using DataTableAssignment.Web.Models.Enitites;
using System.Text.Json;

namespace DataTableAssignment.Web.Mapping
{
    public class AutomapperProfiles : Profile
    {
       
        public AutomapperProfiles()
        {
            // Mapping from Employee to EmployeeDto
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.EmployeeDetailsDto, opt => opt.MapFrom(src => src.EmployeeDetails));

            // Mapping from EmployeeDetails to EmployeeDetailsDto
            CreateMap<EmployeeDetails, EmployeeDetailsDto>()
                .ForMember(dest => dest.EmployeeBenefitsDto, opt => opt.MapFrom(src => src.EmployeeBenefits));

            // Mapping from EmployeeBenefits to EmployeeBenefitsDto
            CreateMap<EmployeeBenefits, EmployeeBenefitsDto>();

            // Mapping from EmployeeDto to EmployeeViewModel
            CreateMap<EmployeeDto, EmployeeViewModel>()
            .ForMember(dest => dest.EmployeeDetails, opt => opt.MapFrom(src => src.EmployeeDetailsDto));

            //CreateMap<EmployeeDto, EmployeeViewModel>()
            //    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.EmployeeDetailsDto.Address))
            //    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.EmployeeDetailsDto.PhoneNumber));
            //    //.ForMember(dest => dest.BenefitType, opt => opt.MapFrom(src => src.EmployeeDetailsDto.EmployeeBenefitsDto.BenefitType))
            //.ForMember(dest => dest.BenefitValue, opt => opt.MapFrom(src => src.EmployeeDetailsDto.EmployeeBenefitsDto.BenefitValue));

            // Mapping from EmployeeViewModel to EmployeeDto
            CreateMap<EmployeeViewModel, EmployeeDto>()
                .ForMember(dest => dest.EmployeeDetailsDto, opt => opt.MapFrom(src => src.EmployeeDetails))
                .AfterMap((src, dest) =>
                {
                    dest.EmployeeDetailsDto.EmployeeId = src.Id;
                    if (dest.EmployeeDetailsDto.EmployeeBenefitsDto != null)
                    {
                        foreach (var benefit in dest.EmployeeDetailsDto.EmployeeBenefitsDto)
                        {
                            benefit.EmployeeDetailId = src.EmployeeDetails.Id;
                        }
                    }
                });

            // Mapping from EmployeeDetailViewModel to EmployeeDetailsDto
            CreateMap<EmployeeDetailViewModel, EmployeeDetailsDto>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EmployeeBenefitsDto, opt => opt.MapFrom(src => src.EmployeeBenefits));

            // Mapping from EmployeeBenefitsViewModel to EmployeeBenefitsDto
            CreateMap<EmployeeBenefitsViewModel, EmployeeBenefitsDto>()
                .ForMember(dest => dest.EmployeeDetailId, opt => opt.MapFrom(src => src.Id));





            CreateMap<EmployeeDetailsDto, EmployeeDetailViewModel>();


            // Mapping from EmployeeDto to Employee
            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.EmployeeDetails, opt => opt.MapFrom(src => src.EmployeeDetailsDto));


            // Mapping from EmployeeDetailsDto to EmployeeDetails
            CreateMap<EmployeeDetailsDto, EmployeeDetails>()
                .ForMember(dest => dest.EmployeeBenefits, opt => opt.MapFrom(src => src.EmployeeBenefitsDto));

            // Mapping from EmployeeBenefitsDto to EmployeeBenefits
            CreateMap<EmployeeBenefitsDto, EmployeeBenefits>();
                



            // Other mappings
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

            // Employee Mapping
            //CreateMap<Employee, Employee>().ForAllMembers(opt => opt.Ignore());
            //CreateMap<Employee, Employee>()
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //    .ForMember(dest => dest.Office, opt => opt.MapFrom(src => src.Office))
            //    .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
            //    .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            //    .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
            //    .ForMember(dest => dest.EmployeeDetails, opt => opt.Ignore()); // Do not replace directly


            // EmployeeDetails Mapping
            CreateMap<Employee, Employee>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Office, opt => opt.MapFrom(src => src.Office))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForPath(dest => dest.EmployeeDetails.Address, opt => opt.MapFrom(src => src.EmployeeDetails.Address))
                .ForPath(dest => dest.EmployeeDetails.PhoneNumber, opt => opt.MapFrom(src => src.EmployeeDetails.PhoneNumber));
            //.ForPath(dest => dest.EmployeeDetails.EmployeeBenefits.BenefitType, opt => opt.MapFrom(src => src.EmployeeDetails.EmployeeBenefits.BenefitType))
            //.ForPath(dest => dest.EmployeeDetails.EmployeeBenefits.BenefitValue, opt => opt.MapFrom(src => src.EmployeeDetails.EmployeeBenefits.BenefitValue));


            // Mapping from EmployeeWithDetailsAndBenefitsDto to Employee
            // Map EmployeeDto to Employee
            CreateMap<EmployeeWithDetailsAndBenefits, Employee>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.EmployeeDetails, opt => opt.MapFrom(src => src));

            CreateMap<EmployeeWithDetailsAndBenefits, EmployeeDetails>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmployeeDetailsId))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.EmployeeBenefits, opt => opt.MapFrom(src => ParseEmployeeBenefits(src.EmployeeBenefits)));

            CreateMap<EmployeeBenefits, EmployeeBenefits>(); // For JSON deserialization into EmployeeBenefits objects.


            CreateMap<EmployeeDetailsDto, EmployeeDetailViewModel>()
                .ForMember(dest => dest.EmployeeBenefits, opt => opt.MapFrom(src => src.EmployeeBenefitsDto));
            CreateMap<EmployeeBenefitsDto, EmployeeBenefitsViewModel>();
           

        }
        private static ICollection<EmployeeBenefits> ParseEmployeeBenefits(string employeeBenefitsJson)
        {
            if (string.IsNullOrEmpty(employeeBenefitsJson))
                return new List<EmployeeBenefits>();

            try
            {
                return JsonSerializer.Deserialize<List<EmployeeBenefits>>(employeeBenefitsJson) ?? new List<EmployeeBenefits>();
            }
            catch
            {
                // Handle invalid JSON if needed
                return new List<EmployeeBenefits>();
            }
        }
    }
}
