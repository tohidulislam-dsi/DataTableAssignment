using DataTableAssignment.Web.Models.Dto;
using DataTableAssignment.Web.Models.Response;
using DataTableAssignment.Web.Models.ViewModel;
public interface IEmployeeService
{
    Task<EmployeeFilterResultDto<EmployeeDto>> GetFilteredEmployeesAsync(EmployeeListRequestModel requestData);
    Task AddEmployeeAsync(EmployeeViewModel employeeViewModel);

    Task<EmployeeViewModel?> GetEmployeeById(Guid id);
    Task UpdateEmployeeAsync(EmployeeViewModel employeeViewModel);
    Task DeleteEmployeeById(Guid id);
}
