using DataTableAssignment.Web.Models.Dto;
using DataTableAssignment.Web.Models.Response;
using DataTableAssignment.Web.Models.ViewModel;
using DataTableAssignment.Web.Models.Entities;
public interface IEmployeeService
{
    Task<EmployeeFilterResultDto<EmployeeDto>> GetFilteredEmployeesAsync(EmployeeListRequestModel requestData);
    Task<Guid> AddEmployeeAsync(EmployeeDto employeeDto);

    Task<EmployeeViewModel?> GetEmployeeById(Guid id);
    Task UpdateEmployeeAsync(EmployeeDto employeeDto);
    Task DeleteEmployeeById(Guid id);
}
