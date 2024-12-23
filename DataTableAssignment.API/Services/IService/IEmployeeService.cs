using DataTableAssignment.API.Models.Dto;
using DataTableAssignment.API.Models.Response;
public interface IEmployeeService
{
    Task<EmployeeFilterResultDto<EmployeeDto>> GetFilteredEmployeesAsync(EmployeeListRequestModel requestData);
}
