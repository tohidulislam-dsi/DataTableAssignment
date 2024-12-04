using DataTableAssignment.Web.Models.Dto;
public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetFilteredEmployeesAsync(DataTableRequestDto requestData);
}
