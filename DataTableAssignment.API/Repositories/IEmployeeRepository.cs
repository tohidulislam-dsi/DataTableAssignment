using DataTableAssignment.API.Models.Domain;
using DataTableAssignment.API.Data;
using Microsoft.EntityFrameworkCore;
using DataTableAssignment.API.Models.Dto;

namespace DataTableAssignment.API.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(long id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(long id);
        Task<EmployeeFilterResultDto<Employee>> GetFilteredEmployeesAsync(EmployeeListRequestModel requestData);
        Task<int> GetTotalEmployeeCountAsync();

    }
}

