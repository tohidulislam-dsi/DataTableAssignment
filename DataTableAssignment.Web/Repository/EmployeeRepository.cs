using DataTableAssignment.Web.Models.Domain;
using DataTableAssignment.Web.Data;
using Microsoft.EntityFrameworkCore;
public class EmployeeRepository : IEmployeeRepository
{
    private readonly DataTableAssignmentDbContext dbContext;

    public EmployeeRepository(DataTableAssignmentDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await dbContext.Employees.ToListAsync();
    }

    public async Task<Employee> GetByIdAsync(long id)
    {
        return await dbContext.Employees.FindAsync(id);
    }

    public async Task AddAsync(Employee employee)
    {
        await dbContext.Employees.AddAsync(employee);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Employee employee)
    {
        dbContext.Employees.Update(employee);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var employee = await dbContext.Employees.FindAsync(id);
        if (employee != null)
        {
            dbContext.Employees.Remove(employee);
            await dbContext.SaveChangesAsync();
        }
    }
    public IQueryable<Employee> GetAllAsQueryable()
    {
        return dbContext.Employees.AsQueryable();
    }

}

