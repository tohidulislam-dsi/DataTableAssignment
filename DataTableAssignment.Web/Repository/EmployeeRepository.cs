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

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        return await dbContext.Employees.FindAsync(id);
    }

    public async Task<EmployeeDetails?> GetEmployeeDetailsByEmployeeIdAsync(Guid employeeId)
    {
        return await dbContext.EmployeeDetails.FirstOrDefaultAsync(ed => ed.EmployeeId == employeeId);
    }

    public async Task<EmployeeBenefits?> GetEmployeeBenefitsByEmployeeDetailsIdASync(Guid employeeDetailsId)
    {
        return await dbContext.EmployeeBenefits.FirstOrDefaultAsync(ed => ed.EmployeeDetailId == employeeDetailsId);

    }

    public async Task<Guid> AddAsync(Employee employee, EmployeeDetails employeeDetails, EmployeeBenefits employeeBenefits)
    {
        using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            employeeDetails.EmployeeId = employee.Id;
            await dbContext.EmployeeDetails.AddAsync(employeeDetails);
            await dbContext.SaveChangesAsync();

            employeeBenefits.EmployeeDetailId = employeeDetails.Id;
            await dbContext.EmployeeBenefits.AddAsync(employeeBenefits);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return employee.Id;
        }
        catch(Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }

    }

    public async Task UpdateAsync(Employee employee, EmployeeDetails employeeDetails, EmployeeBenefits employeeBenefits)
    {
        using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            // Update Employee
            dbContext.Employees.Update(employee);
            await dbContext.SaveChangesAsync();

            // Update EmployeeDetails
            var employeeDetailsId = await dbContext.EmployeeDetails
            .Where(ed => ed.EmployeeId == employee.Id)
            .Select(ed => ed.Id)
            .FirstOrDefaultAsync();
            employeeDetails.Id = employeeDetailsId;
            employeeDetails.EmployeeId = employee.Id;
            
            dbContext.EmployeeDetails.Update(employeeDetails);
            await dbContext.SaveChangesAsync();


            var employeeBenefitsId = await dbContext.EmployeeBenefits
            .Where(ed => ed.EmployeeDetailId == employeeDetailsId)
            .Select(ed => ed.Id)
            .FirstOrDefaultAsync();
            
            // Update EmployeeBenefits
            employeeBenefits.Id = employeeBenefitsId;
            employeeBenefits.EmployeeDetailId = employeeDetailsId;
            dbContext.EmployeeBenefits.Update(employeeBenefits);
            await dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteAsync(Guid id)
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

