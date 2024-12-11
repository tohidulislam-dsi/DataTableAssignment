using DataTableAssignment.Web.Models.Domain;
using DataTableAssignment.Web.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using DataTableAssignment.Web.Models.Dto;
using Microsoft.Data.SqlClient;
using System.Data;

public class EmployeeRepositoryWithStoredProcedures : IEmployeeRepository
{
    private readonly DataTableAssignmentDbContext dbContext;

    public EmployeeRepositoryWithStoredProcedures(DataTableAssignmentDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await dbContext.Employees
            .FromSqlRaw("EXEC GetAllEmployees")
            .ToListAsync();
    }

    public async Task<Employee> GetByIdAsync(long id)
    {
        var parameter = new SqlParameter("@Id", id);
        return await dbContext.Employees
            .FromSqlRaw("EXEC GetEmployeeById @Id", parameter)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(Employee employee)
    {
        var parameters = new[]
        {
            new SqlParameter("@Name", employee.Name),
            new SqlParameter("@Position", employee.Position),
            new SqlParameter("@Office", employee.Office),
            new SqlParameter("@Age", employee.Age),
            new SqlParameter("@Salary", employee.Salary)
        };

        await dbContext.Database.ExecuteSqlRawAsync("EXEC AddEmployee @Name, @Position, @Office, @Age, @Salary", parameters);
    }

    public async Task UpdateAsync(Employee employee)
    {
        var parameters = new[]
        {
            new SqlParameter("@Id", employee.Id),
            new SqlParameter("@Name", employee.Name),
            new SqlParameter("@Position", employee.Position),
            new SqlParameter("@Office", employee.Office),
            new SqlParameter("@Age", employee.Age),
            new SqlParameter("@Salary", employee.Salary)
        };

        await dbContext.Database.ExecuteSqlRawAsync("EXEC UpdateEmployee @Id, @Name, @Position, @Office, @Age, @Salary", parameters);
    }

    public async Task DeleteAsync(long id)
    {
        var parameter = new SqlParameter("@Id", id);
        await dbContext.Database.ExecuteSqlRawAsync("EXEC DeleteEmployee @Id", parameter);
    }

    public async Task<int> GetTotalEmployeeCountAsync()
    {
        return await dbContext.Employees.CountAsync();
    }

    public async Task<EmployeeFilterResultDto<Employee>> GetFilteredEmployeesAsync(EmployeeListRequestModel requestData)
    {
        var searchValue = new SqlParameter("@SearchValue", requestData.Search.Value ?? (object)DBNull.Value);
        var start = new SqlParameter("@Start", requestData.Start);
        var length = new SqlParameter("@Length", requestData.Length);
        var orderBy = new SqlParameter("@OrderBy", string.Join(", ", requestData.Order.Select(o =>
        {
            var columnName = requestData.Columns[o.Column].Name;
            var direction = o.Dir;
            return $"{columnName} {direction}";
        })));

        var name = new SqlParameter("@Name", requestData.Columns.FirstOrDefault(c => c.Name == "name")?.Search.Value ?? (object)DBNull.Value);
        var position = new SqlParameter("@Position", requestData.Columns.FirstOrDefault(c => c.Name == "position")?.Search.Value ?? (object)DBNull.Value);
        var office = new SqlParameter("@Office", requestData.Columns.FirstOrDefault(c => c.Name == "office")?.Search.Value ?? (object)DBNull.Value);
        var age = new SqlParameter("@Age", requestData.Columns.FirstOrDefault(c => c.Name == "age")?.Search.Value ?? (object)DBNull.Value);
        var salary = new SqlParameter("@Salary", requestData.Columns.FirstOrDefault(c => c.Name == "salary")?.Search.Value ?? (object)DBNull.Value);

        var totalEmployeesParam = new SqlParameter
        {
            ParameterName = "@TotalEmployees",
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Output
        };

        var totalFilteredRecordsParam = new SqlParameter
        {
            ParameterName = "@TotalFilteredRecords",
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Output
        };

        var employees = await dbContext.Employees
            .FromSqlRaw("EXEC GetFilteredEmployees @SearchValue, @Start, @Length, @OrderBy, @Name, @Position, @Office, @Age, @Salary, @TotalEmployees OUTPUT, @TotalFilteredRecords OUTPUT",
                        searchValue, start, length, orderBy, name, position, office, age, salary, totalEmployeesParam, totalFilteredRecordsParam)
            .ToListAsync();

        var totalEmployees = (int)totalEmployeesParam.Value;
        var totalFilteredRecords = (int)totalFilteredRecordsParam.Value;
        return new EmployeeFilterResultDto<Employee>
        {
            data = employees,
            recordsFiltered = totalFilteredRecords,
            recordsTotal = totalEmployees
        };
    }
}
