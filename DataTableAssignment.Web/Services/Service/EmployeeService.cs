using System.Linq;
using AutoMapper;
using DataTableAssignment.Web.Models.Dto;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using DataTableAssignment.Web.Models.Domain;
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

    public async Task<Guid> AddEmployeeAsync(EmployeeDto employeeDto, EmployeeDetailsDto employeeDetailsDto, EmployeeBenefitsDto employeeBenefitsDto)
    {
        var employee = mapper.Map<Employee>(employeeDto);
        var employeeDetails = mapper.Map<EmployeeDetails>(employeeDetailsDto);
        var employeeBenefits = mapper.Map<EmployeeBenefits>(employeeBenefitsDto);

        var employeeId = await employeeRepository.AddAsync(employee, employeeDetails, employeeBenefits);
        return employeeId;
    }

    public async Task<EmployeeViewModel?> GetEmployeeById(Guid Id)
    {
        var employee = await employeeRepository.GetByIdAsync(Id);
        var employeeDetails = await employeeRepository.GetEmployeeDetailsByEmployeeIdAsync(employee.Id);
        var employeeBenefits = await employeeRepository.GetEmployeeBenefitsByEmployeeDetailsIdASync(employeeDetails.Id);
        var employeeDto = mapper.Map<EmployeeDto>(employee);
        var employeeDetailsDto = mapper.Map<EmployeeDetailsDto>(employeeDetails);
        var employeeBenefitsDto = mapper.Map<EmployeeBenefitsDto>(employeeBenefits);

        var employeeViewModel = mapper.Map<EmployeeViewModel>((employeeDto, employeeDetailsDto, employeeBenefitsDto));

        return employeeViewModel;
    }

    public async Task UpdateEmployeeAsync(EmployeeDto employeeDto, EmployeeDetailsDto employeeDetailsDto, EmployeeBenefitsDto employeeBenefitsDto)
    {
        var employee = mapper.Map<Employee>(employeeDto);
        var employeeDetails = mapper.Map<EmployeeDetails>(employeeDetailsDto);
        var employeeBenefits = mapper.Map<EmployeeBenefits>(employeeBenefitsDto);
        await employeeRepository.UpdateAsync(employee, employeeDetails, employeeBenefits);
    }

    public async Task DeleteEmployeeById(Guid id)
    {
        await employeeRepository.DeleteAsync(id);
    }
}
