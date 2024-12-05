using DataTableAssignment.Web.Models.Dto;
using DataTableAssignment.Web.Models.Response;
public interface IEmployeeService
{
    Task<EmployeeFilterResultDto> GetFilteredEmployeesAsync(EmployeeListRequestModel requestData);
}
