﻿using AutoMapper;
using DataTableAssignment.API.Models.Domain;
using DataTableAssignment.API.Models.Dto;
using DataTableAssignment.API.Models.Response;
using DataTableAssignment.API.Models.ViewModel;

namespace DataTableAssignment.API.Mapping
{
    public class AutomapperProfiles:Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Employee,EmployeeDto>().ReverseMap();
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
