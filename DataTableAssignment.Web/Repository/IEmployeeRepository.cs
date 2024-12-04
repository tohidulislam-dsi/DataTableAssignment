using DataTableAssignment.Web.Models.Domain;
using DataTableAssignment.Web.Data;
using Microsoft.EntityFrameworkCore;
public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee> GetByIdAsync(long id);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(long id);
    
}

