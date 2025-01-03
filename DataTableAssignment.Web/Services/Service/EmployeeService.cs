using System.Linq;
using AutoMapper;
using DataTableAssignment.Web.Models.Dto;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using DataTableAssignment.Web.Models.Entities;
using Azure.Core;
using DataTableAssignment.Web.Models.Response;
using DataTableAssignment.Web.Models.ViewModel;
using System.Runtime.InteropServices;


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

    public async Task<Guid> AddEmployeeAsync(EmployeeDto employeeDto)
    {
        var employee = mapper.Map<Employee>(employeeDto);

        var employeeId = await employeeRepository.AddAsync(employee);
        return employeeId;
    }

    public async Task<EmployeeViewModel?> GetEmployeeById(Guid Id)
    {
        var employee = await employeeRepository.GetByIdAsync(Id);

        var employeeDto = mapper.Map<EmployeeDto>(employee);
        var employeeViewModel = mapper.Map<EmployeeViewModel>(employeeDto);

        return employeeViewModel;
    }

    public async Task UpdateEmployeeAsync(EmployeeDto employeeDto)
    {
        var employee = mapper.Map<Employee>(employeeDto);
        await employeeRepository.UpdateAsync(employee);
    }

    public async Task DeleteEmployeeById(Guid id)
    {
        await employeeRepository.DeleteAsync(id);
    }
}
