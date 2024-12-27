using DataTableAssignment.Web.Models.Dto;
using DataTableAssignment.Web.Models.Response;
using DataTableAssignment.Web.Models.ViewModel;
using DataTableAssignment.Web.Models.Domain;
public interface IEmployeeService
{
    Task<EmployeeFilterResultDto<EmployeeDto>> GetFilteredEmployeesAsync(EmployeeListRequestModel requestData);
    Task<Employee> AddEmployeeAsync(EmployeeDto employeeDto);

    Task<EmployeeViewModel?> GetEmployeeById(Guid id);
    Task UpdateEmployeeAsync(EmployeeDto employeeDto);
    Task DeleteEmployeeById(Guid id);
}
