using System.Linq;
using AutoMapper;
using DataTableAssignment.Web.Models.Dto;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using DataTableAssignment.Web.Models.Domain;
using Azure.Core;
using DataTableAssignment.Web.Models.Response;
using DataTableAssignment.Web.Models.ViewModel;


public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IMapper mapper;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        this.employeeRepository = employeeRepository;
        this.mapper = mapper;
    }

    public async Task<EmployeeFilterResultDto<EmployeeDto>> GetFilteredEmployeesAsync(EmployeeListRequestModel requestData)
    {
        var filteredEmployees = await employeeRepository.GetFilteredEmployeesAsync(requestData);

        var response = mapper.Map<EmployeeFilterResultDto<EmployeeDto>>(filteredEmployees);


        return response;

    }

    public async Task AddEmployeeAsync(EmployeeViewModel employeeViewModel)
    {
        var employeeDto = mapper.Map<EmployeeDto>(employeeViewModel);
        var employee = mapper.Map<Employee>(employeeDto);
        await employeeRepository.AddAsync(employee);
    }
}
