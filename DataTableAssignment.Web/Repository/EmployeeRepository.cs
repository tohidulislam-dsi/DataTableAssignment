using DataTableAssignment.Web.Models.Domain;
using DataTableAssignment.Web.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using DataTableAssignment.Web.Models.Dto;
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
    public async Task<int> GetTotalEmployeeCountAsync()
    {
        return await dbContext.Employees.CountAsync();
    }
    public async Task<EmployeeFilterResultDto<Employee>> GetFilteredEmployeesAsync(EmployeeListRequestModel requestData)
    {
        var query = dbContext.Employees.AsQueryable();
        var totalEmployees = await query.CountAsync();

        // Global search filter
        if (!string.IsNullOrEmpty(requestData.Search.Value))

        {
            var globalSearchValue = requestData.Search.Value.ToLower();
            query = query.Where(x => x.Name.ToLower().Contains(globalSearchValue) ||
                                     x.Position.ToLower().Contains(globalSearchValue) ||
                                     x.Office.ToLower().Contains(globalSearchValue) ||
                                     x.Age.ToString().Contains(globalSearchValue) ||
                                     x.Salary.ToString().Contains(globalSearchValue));
        }

        // Per column search filter
        foreach (var column in requestData.Columns)
        {
            if (!string.IsNullOrEmpty(column.Search.Value))
            {
                var searchValue = column.Search.Value.ToLower();
                switch (column.Name)
                {
                    case "name":
                        query = query.Where(x => x.Name.ToLower().Contains(searchValue));
                        break;
                    case "position":
                        query = query.Where(x => x.Position.ToLower().Contains(searchValue));
                        break;
                    case "office":
                        query = query.Where(x => x.Office.ToLower().Contains(searchValue));
                        break;
                    case "age":
                        query = query.Where(x => x.Age.ToString().Contains(searchValue));
                        break;
                    case "salary":
                        query = query.Where(x => x.Salary.ToString().Contains(searchValue));
                        break;
                }
            }
        }
        int start = (int)requestData.Start;
        int length = (int)requestData.Length;


        // Sorting

        if (requestData.Order != null && requestData.Order.Count > 0)
        {
            var ordering = string.Join(", ", requestData.Order.Select(o =>
            {
                var columnName = requestData.Columns[o.Column].Name;
                var direction = o.Dir;
                return $"{columnName} {direction}";
            }));
            query = query.OrderBy(ordering);
        }

        var totalFilteredRecords = await query.CountAsync();
        


        // Paging
        query = query.Skip(start).Take(length);

        return new EmployeeFilterResultDto<Employee>
        {
            data = await query.ToListAsync(),
            recordsFiltered = totalFilteredRecords,
            recordsTotal = totalEmployees
        };
        
    }

}

