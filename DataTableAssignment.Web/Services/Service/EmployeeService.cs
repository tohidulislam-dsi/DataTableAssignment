using System.Linq;
using AutoMapper;
using DataTableAssignment.Web.Models.Dto;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using DataTableAssignment.Web.Models.Domain;
using Azure.Core;
using DataTableAssignment.Web.Models.Response;


public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IMapper mapper;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        this.employeeRepository = employeeRepository;
        this.mapper = mapper;
    }

    public async Task<EmployeeFilterResultDto> GetFilteredEmployeesAsync(EmployeeListRequestModel requestData)
    {
        var filteredEmployees = await employeeRepository.GetFilteredEmployeesAsync(requestData);
        var empList = mapper.Map<IEnumerable<EmployeeDto>>(filteredEmployees.Employees);
        var totalFilteredRecords = filteredEmployees.TotalFilteredRecords;
        var totalRecords = await employeeRepository.GetTotalEmployeeCountAsync();

        var employeeFilterResponse = new EmployeeFilterResultDto()
        {
            TotalRecords = totalRecords,
            TotalFilteredRecords = totalFilteredRecords,
            Employees = empList
        };

        return employeeFilterResponse;
    }
}
