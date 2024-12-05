using DataTableAssignment.Web.Models.Dto;
using DataTableAssignment.Web.Models.Response;
public interface IEmployeeService
{
    Task<EmployeeFilterResponseModel> GetFilteredEmployeesAsync(EmployeeListRequestModel requestData);
}
