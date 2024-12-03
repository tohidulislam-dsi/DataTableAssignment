using DataTableAssignment.API.Models.Domain;    
using DataTableAssignment.API.Data;
using Microsoft.EntityFrameworkCore;

namespace DataTableAssignment.API.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly DataTableAssignmentDbContext dbcontext;
        public EmployeeRepository(DataTableAssignmentDbContext dbContext)
        {
            this.dbcontext = dbContext;

        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            await dbcontext.Employees.AddAsync(employee);
            await dbcontext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> DeleteEmployeeAsync(long id)
        {
            var employee = await dbcontext.Employees.FindAsync(id);
            if (employee == null)
            {
                return null;
            }
            dbcontext.Employees.Remove(employee);
            await dbcontext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> GetEmployeeByIdAsync(long id)
        {
            return await dbcontext.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await dbcontext.Employees.ToListAsync();
        }   

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            dbcontext.Employees.Update(employee);
            await dbcontext.SaveChangesAsync();
            return employee;
        }   
    }
}
