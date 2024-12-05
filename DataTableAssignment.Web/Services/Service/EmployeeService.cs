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

    public async Task<EmployeeFilterResponseModel> GetFilteredEmployeesAsync(EmployeeListRequestModel requestData)
    {
        var filteredEmployees = await employeeRepository.GetFilteredEmployeesAsync(requestData);
        var empList = mapper.Map<IEnumerable<EmployeeDto>>(filteredEmployees.Employees);
        var totalFilteredRecords = filteredEmployees.TotalFilteredRecords;
        var response = new EmployeeFilterResponseModel()

        {
            draw = requestData.Draw,
            recordsTotal = empList.Count(),
            recordsFiltered = totalFilteredRecords,
            data = empList.ToList()
        };

        return response;
    }
}
