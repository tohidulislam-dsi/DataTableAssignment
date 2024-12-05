using System.Linq;
using AutoMapper;
using DataTableAssignment.Web.Models.Dto;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using DataTableAssignment.Web.Models.Domain;
using Azure.Core;


public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IMapper mapper;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        this.employeeRepository = employeeRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeDto>> GetFilteredEmployeesAsync(EmployeeListRequestModel requestData)
    {
        var query = employeeRepository.GetAllAsQueryable();

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

        // Per Column filter
        //var searchActions = new Dictionary<string, Func<Employee, string, bool>>
        //{
        //    { "name", (x, value) => x.Name.ToLower().Contains(value.ToLower()) },
        //    { "position", (x, value) => x.Position.ToLower().Contains(value.ToLower()) },
        //    { "office", (x, value) => x.Office.ToLower().Contains(value.ToLower()) },
        //    { "age", (x, value) => x.Age.ToString().Contains(value.ToLower()) },
        //    { "salary", (x, value) => x.Salary.ToString().Contains(value.ToLower()) }
        //};

        //foreach (var column in requestData.Columns)
        //{
        //    if (!string.IsNullOrEmpty(column.Search.Value) && searchActions.ContainsKey(column.Name))
        //    {
        //        var searchValue = column.Search.Value.ToLower();
        //        query = query.Where(x => searchActions[column.Name](x, searchValue));
        //    }
        //}

        var empList = await query.ToListAsync();
        return mapper.Map<IEnumerable<EmployeeDto>>(empList);


        //var employeeList = await employeeRepository.GetAllAsync();
        //var empList = mapper.Map<IEnumerable<EmployeeDto>>(employeeList);

        //// Global search filter
        //if (!string.IsNullOrEmpty(requestData.Search.Value))
        //{
        //    empList = empList.Where(x => x.Name.ToLower().Contains(requestData.Search.Value.ToLower()) ||
        //                                 x.Position.ToLower().Contains(requestData.Search.Value.ToLower()) ||
        //                                 x.Office.ToLower().Contains(requestData.Search.Value.ToLower()) ||
        //                                 x.Age.ToString().Contains(requestData.Search.Value.ToLower()) ||
        //                                 x.Salary.ToString().Contains(requestData.Search.Value.ToLower()));
        //}

        //// Per Column filter
        //var searchActions = new Dictionary<string, Func<EmployeeDto, string, bool>>
        //{
        //    { "name", (x, value) => x.Name.ToLower().Contains(value.ToLower()) },
        //    { "position", (x, value) => x.Position.ToLower().Contains(value.ToLower()) },
        //    { "office", (x, value) => x.Office.ToLower().Contains(value.ToLower()) },
        //    { "age", (x, value) => x.Age.ToString().Contains(value.ToLower()) },
        //    { "salary", (x, value) => x.Salary.ToString().Contains(value.ToLower()) }
        //};

        //foreach (var column in requestData.Columns)
        //{
        //    if (!string.IsNullOrEmpty(column.Search.Value) && searchActions.ContainsKey(column.Name))
        //    {
        //        var searchValue = column.Search.Value.ToLower();
        //        empList = empList.Where(x => searchActions[column.Name](x, searchValue));
        //    }
        //}


        //return empList;
    }
}
