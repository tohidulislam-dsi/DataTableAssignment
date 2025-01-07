using DataTableAssignment.Web.Models.Entities;
using DataTableAssignment.Web.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using DataTableAssignment.Web.Models.Dto;
using Microsoft.Data.SqlClient;
using System.Data;
using AutoMapper;
using DataTableAssignment.Web.Models.Enitites;

public class EmployeeRepositoryWithStoredProcedures : IEmployeeRepository
{
    private readonly DataTableAssignmentDbContext dbContext;
    private readonly IMapper mapper;

    public EmployeeRepositoryWithStoredProcedures(DataTableAssignmentDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await dbContext.Employees
            .FromSqlRaw("EXEC GetAllEmployees")
            .ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        var parameter = new SqlParameter("@EmployeeId", id);
        var result = await dbContext.Set<EmployeeWithDetailsAndBenefits>()
            .FromSqlRaw("EXEC GetEmployeeById @EmployeeId", parameter)
            .ToListAsync();
        var dto = result.FirstOrDefault();
        var employee = mapper.Map<Employee>(dto);
        return employee;
    }
    public async Task<EmployeeDetails?> GetEmployeeDetailsByEmployeeIdAsync(Guid employeeId)
    {
        //NotImplementedException();
        var parameter = new SqlParameter("@EmployeeId", employeeId);
        var result = await dbContext.EmployeeDetails
            .FromSqlRaw("EXEC GetEmployeeDetailsByEmployeeId @EmployeeId", parameter)
            .ToListAsync();
        return result.FirstOrDefault();
    }

    public async Task<EmployeeBenefits?> GetEmployeeBenefitsByEmployeeDetailsIdASync(Guid employeeDetailsId)
    {
        var parameter = new SqlParameter("@EmployeeDetailId", employeeDetailsId);
        var result = await dbContext.EmployeeBenefits
            .FromSqlRaw("EXEC GetEmployeeBenefitsByEmployeeDetailsId @EmployeeDetailId", parameter)
            .ToListAsync();
        return result.FirstOrDefault();
    }
    public async Task<Guid> AddAsync(Employee employee)
    {
        var idParameter = new SqlParameter
        {
            ParameterName = "@EmployeeId",
            SqlDbType = SqlDbType.UniqueIdentifier,
            Direction = ParameterDirection.Output
        };
        var benefitsTable = new DataTable();
        benefitsTable.Columns.Add("BenefitType", typeof(string));
        benefitsTable.Columns.Add("BenefitValue", typeof(int));

        if (employee.EmployeeDetails?.EmployeeBenefits != null)
        {
            foreach (var benefit in employee.EmployeeDetails.EmployeeBenefits)
            {
                benefitsTable.Rows.Add(benefit.BenefitType, benefit.BenefitValue);
            }
        }
        var benefitsParameter = new SqlParameter
        {
            ParameterName = "@EmployeeBenefits",
            SqlDbType = SqlDbType.Structured,
            TypeName = "dbo.EmployeeBenefitType",
            Value = benefitsTable
        };
        var parameters = new List<SqlParameter>
        {
            new SqlParameter("@Name", employee.Name),
            new SqlParameter("@Position", employee.Position),
            new SqlParameter("@Office", employee.Office),
            new SqlParameter("@Age", employee.Age),
            new SqlParameter("@Salary", employee.Salary),
            new SqlParameter("@Address", (object)employee.EmployeeDetails?.Address ?? DBNull.Value),
            new SqlParameter("@PhoneNumber", (object)employee.EmployeeDetails?.PhoneNumber ?? DBNull.Value),
            benefitsParameter,
            idParameter
        };

        await dbContext.Database.ExecuteSqlRawAsync(
            "EXEC InsertEmployeeData @Name, @Position, @Office, @Age, @Salary, @Address, @PhoneNumber, @EmployeeBenefits, @EmployeeId OUTPUT",
            parameters
        );

        return (Guid)idParameter.Value;
    }

    public async Task UpdateAsync(Employee employee)
    {
        var parameters = new List<SqlParameter>
        {
            new SqlParameter("@EmployeeId", employee.Id),
            new SqlParameter("@Name", employee.Name),
            new SqlParameter("@Position", employee.Position),
            new SqlParameter("@Office", employee.Office),
            new SqlParameter("@Age", employee.Age),
            new SqlParameter("@Salary", employee.Salary),
            new SqlParameter("@Address", employee.EmployeeDetails.Address),
            new SqlParameter("@PhoneNumber", employee.EmployeeDetails.PhoneNumber),
            new SqlParameter("@BenefitType", employee.EmployeeDetails.EmployeeBenefits.BenefitType),
            new SqlParameter("@BenefitValue", employee.EmployeeDetails.EmployeeBenefits.BenefitValue)

        };

        await dbContext.Database.ExecuteSqlRawAsync(
            "EXEC UpdateEmployeeData @EmployeeId, @Name, @Position, @Office, @Age, @Salary, @Address, @PhoneNumber, @BenefitType, @BenefitValue", 
            parameters);
    }

    public async Task DeleteAsync(Guid id)
    {
        var parameter = new SqlParameter("@EmployeeId", id);
        await dbContext.Database.ExecuteSqlRawAsync("EXEC DeleteEmployeeInformation @EmployeeId", parameter);
    }

    public async Task<int> GetTotalEmployeeCountAsync()
    {
        return await dbContext.Employees.CountAsync();
    }
    private DataTable CreateEmployeeFilterDataTable(EmployeeListRequestModel requestData)
    {
        var table = new DataTable();
        table.Columns.Add("SearchValue", typeof(string));
        table.Columns.Add("Start", typeof(int));
        table.Columns.Add("Length", typeof(int));
        table.Columns.Add("OrderBy", typeof(string));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("Position", typeof(string));
        table.Columns.Add("Office", typeof(string));
        table.Columns.Add("Age", typeof(int));
        table.Columns.Add("Salary", typeof(int));

        var row = table.NewRow();
        row["SearchValue"] = requestData.Search.Value ?? (object)DBNull.Value;
        row["Start"] = requestData.Start ?? (object)DBNull.Value;
        row["Length"] = requestData.Length ?? (object)DBNull.Value;
        row["OrderBy"] = string.Join(", ", requestData.Order.Select(o =>
        {
            var columnName = requestData.Columns[o.Column].Name;
            var direction = o.Dir;
            return $"{columnName} {direction}";
        }));
        row["Name"] = requestData.Columns.FirstOrDefault(c => c.Name == "name")?.Search.Value ?? (object)DBNull.Value;
        row["Position"] = requestData.Columns.FirstOrDefault(c => c.Name == "position")?.Search.Value ?? (object)DBNull.Value;
        row["Office"] = requestData.Columns.FirstOrDefault(c => c.Name == "office")?.Search.Value ?? (object)DBNull.Value;
        row["Age"] = requestData.Columns.FirstOrDefault(c => c.Name == "age")?.Search.Value ?? (object)DBNull.Value;
        row["Salary"] = requestData.Columns.FirstOrDefault(c => c.Name == "salary")?.Search.Value ?? (object)DBNull.Value;

        table.Rows.Add(row);
        return table;
    }
    public async Task<EmployeeFilterResultDto<Employee>> GetFilteredEmployeesAsync(EmployeeListRequestModel requestData)
    {
        var dataTable = CreateEmployeeFilterDataTable(requestData);

         var totalEmployeesParam = new SqlParameter
        {
            ParameterName = "@TotalEmployees",
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Output
        };
        var dataTableParam = new SqlParameter
        {
            ParameterName = "@EmployeeFilterData",
            SqlDbType = SqlDbType.Structured,
            TypeName = "dbo.EmployeeFilterType",
            Value = dataTable
        };

        var parameters = new object[]
        {
            dataTableParam,
            totalEmployeesParam
        };

        var filteredEmployees = await dbContext.EmployeeWithTotalFilteredRecords
        .FromSqlRaw("EXEC GetFilteredEmployees @EmployeeFilterData, @TotalEmployees OUTPUT",
                   parameters)
        .ToListAsync();
        var totalFilteredRecords = 0;
        var totalEmployees = totalEmployeesParam.Value != DBNull.Value ? (int)totalEmployeesParam.Value : 0;
        if (filteredEmployees.Count > 0)
        {
            totalFilteredRecords = filteredEmployees.First().TotalFilteredRecords;
        }
       
        
        var employees = mapper.Map<List<Employee>>(filteredEmployees);

        // Log the values for debugging
        Console.WriteLine($"Total Employees: {totalEmployees}");
        Console.WriteLine($"Total Filtered Records: {totalFilteredRecords}");

        return new EmployeeFilterResultDto<Employee>
        {
            data = employees,
            recordsFiltered = totalFilteredRecords,
            recordsTotal = totalEmployees
        };
    }
}
