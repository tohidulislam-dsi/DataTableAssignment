using System.Linq;
using AutoMapper;
using DataTableAssignment.Web.Models.Dto;
using System.Linq.Dynamic.Core;
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IMapper mapper;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        this.employeeRepository = employeeRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeDto>> GetFilteredEmployeesAsync(DataTableRequestDto requestData)
    {
        var employeeList = await employeeRepository.GetAllAsync();
        var empList = mapper.Map<IEnumerable<EmployeeDto>>(employeeList);

        // Global search filter
        if (!string.IsNullOrEmpty(requestData.Search.Value))
        {
            empList = empList.Where(x => x.Name.ToLower().Contains(requestData.Search.Value.ToLower()) ||
                                         x.Position.ToLower().Contains(requestData.Search.Value.ToLower()) ||
                                         x.Office.ToLower().Contains(requestData.Search.Value.ToLower()) ||
                                         x.Age.ToString().Contains(requestData.Search.Value.ToLower()) ||
                                         x.Salary.ToString().Contains(requestData.Search.Value.ToLower()));
        }

        // Per Column filter
        var searchActions = new Dictionary<string, Func<EmployeeDto, string, bool>>
        {
            { "name", (x, value) => x.Name.ToLower().Contains(value.ToLower()) },
            { "position", (x, value) => x.Position.ToLower().Contains(value.ToLower()) },
            { "office", (x, value) => x.Office.ToLower().Contains(value.ToLower()) },
            { "age", (x, value) => x.Age.ToString().Contains(value.ToLower()) },
            { "salary", (x, value) => x.Salary.ToString().Contains(value.ToLower()) }
        };

        foreach (var column in requestData.Columns)
        {
            if (!string.IsNullOrEmpty(column.Search.Value) && searchActions.ContainsKey(column.Name))
            {
                var searchValue = column.Search.Value.ToLower();
                empList = empList.Where(x => searchActions[column.Name](x, searchValue));
            }
        }


        return empList;
    }
}
