using DataTableAssignment.Web.Models.Entities;
using DataTableAssignment.Web.Data;
using Microsoft.EntityFrameworkCore;
using DataTableAssignment.Web.Models.Dto;
public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(Guid id);
    Task<EmployeeBenefits?> GetEmployeeBenefitsByEmployeeDetailsIdASync(Guid employeeDetailId);
    Task<EmployeeDetails?> GetEmployeeDetailsByEmployeeIdAsync(Guid employeeId);
    Task<Guid> AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(Guid id);
    Task<EmployeeFilterResultDto<Employee>> GetFilteredEmployeesAsync(EmployeeListRequestModel requestData);
    Task<int> GetTotalEmployeeCountAsync();

}

