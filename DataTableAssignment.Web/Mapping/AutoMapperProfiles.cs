using AutoMapper;
using DataTableAssignment.Web.Models.Entities;
using DataTableAssignment.Web.Models.Dto;
using DataTableAssignment.Web.Models.Response;
using DataTableAssignment.Web.Models.ViewModel;
using DataTableAssignment.Web.Models.Enitites;

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
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.EmployeeDetailsDto.Address))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.EmployeeDetailsDto.PhoneNumber))
                .ForMember(dest => dest.BenefitType, opt => opt.MapFrom(src => src.EmployeeDetailsDto.EmployeeBenefitsDto.BenefitType))
                .ForMember(dest => dest.BenefitValue, opt => opt.MapFrom(src => src.EmployeeDetailsDto.EmployeeBenefitsDto.BenefitValue));

            // Reverse mapping from EmployeeViewModel to EmployeeDto
            CreateMap<EmployeeViewModel, EmployeeDto>()
                .ForMember(dest => dest.EmployeeDetailsDto, opt => opt.MapFrom(src => src));

            // Mapping from EmployeeViewModel to EmployeeDetailsDto
            CreateMap<EmployeeViewModel, EmployeeDetailsDto>()
                .ForMember(dest => dest.EmployeeBenefitsDto, opt => opt.MapFrom(src => src));

            // Mapping from EmployeeViewModel to EmployeeBenefitsDto
            CreateMap<EmployeeViewModel, EmployeeBenefitsDto>();

            // Mapping from EmployeeDto to Employee
            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.EmployeeDetails, opt => opt.MapFrom(src => src.EmployeeDetailsDto))
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore());

            // Mapping from EmployeeDetailsDto to EmployeeDetails
            CreateMap<EmployeeDetailsDto, EmployeeDetails>()
                .ForMember(dest => dest.EmployeeBenefits, opt => opt.MapFrom(src => src.EmployeeBenefitsDto))
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore());

            // Mapping from EmployeeBenefitsDto to EmployeeBenefits
            CreateMap<EmployeeBenefitsDto, EmployeeBenefits>()
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore());



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
                .ForPath(dest => dest.EmployeeDetails.PhoneNumber, opt => opt.MapFrom(src => src.EmployeeDetails.PhoneNumber))
                .ForPath(dest => dest.EmployeeDetails.EmployeeBenefits.BenefitType, opt => opt.MapFrom(src => src.EmployeeDetails.EmployeeBenefits.BenefitType))
                .ForPath(dest => dest.EmployeeDetails.EmployeeBenefits.BenefitValue, opt => opt.MapFrom(src => src.EmployeeDetails.EmployeeBenefits.BenefitValue));


            // Mapping from EmployeeWithDetailsAndBenefitsDto to Employee
            CreateMap<EmployeeWithDetailsAndBenefits, Employee>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Office, opt => opt.MapFrom(src => src.Office))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForPath(dest => dest.EmployeeDetails.Id, opt => opt.MapFrom(src => src.EmployeeDetailsId))
                .ForPath(dest => dest.EmployeeDetails.Address, opt => opt.MapFrom(src => src.Address))
                .ForPath(dest => dest.EmployeeDetails.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForPath(dest => dest.EmployeeDetails.CreatedOn, opt => opt.MapFrom(src => src.EmployeeDetailsCreatedOn))
                .ForPath(dest => dest.EmployeeDetails.EmployeeBenefits.Id, opt => opt.MapFrom(src => src.EmployeeBenefitsId))
                .ForPath(dest => dest.EmployeeDetails.EmployeeBenefits.BenefitType, opt => opt.MapFrom(src => src.BenefitType))
                .ForPath(dest => dest.EmployeeDetails.EmployeeBenefits.BenefitValue, opt => opt.MapFrom(src => src.BenefitValue))
                .ForPath(dest => dest.EmployeeDetails.EmployeeBenefits.CreatedOn, opt => opt.MapFrom(src => src.EmployeeBenefitsCreatedOn));


        }
    }
}
