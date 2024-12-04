using DataTableAssignment.Web.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Web;
using Microsoft.Extensions.Configuration;
using DataTableAssignment.Web.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Buffers;
using Azure.Core;

namespace DataTableAssignment.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly IEmployeeService employeeService;
        private readonly DataTableAssignmentDbContext dbContext;
        public EmployeeController(DataTableAssignmentDbContext dbContext,  
            IHttpClientFactory httpClientFactory, 
            IConfiguration configuration,
            IEmployeeService employeeService)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.dbContext = dbContext;
            this.employeeService = employeeService;
        }
        public async Task<IActionResult> Index()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployeeList(DataTableRequestDto requestData)
        {
            var employees = await employeeService.GetFilteredEmployeesAsync(requestData);

            var totalRecord = employees.Count();

            int start = requestData.Start;
            int length = requestData.Length;

            string sortColumnName = requestData.Columns[requestData.Order[0].Column].Name;
            string sortDirection = requestData.Order[0].Dir;

            //sorting
            employees = employees.AsQueryable().OrderBy(sortColumnName + " " + sortDirection).ToList();
            var totalFilteredRecoird = employees.Count();

            //paging
            employees = employees.Skip(start).Take(length).ToList();

            var response = new EmployeeFilterResponseDto()

            {
                draw = requestData.Draw,
                recordsTotal = totalRecord,
                recordsFiltered = totalFilteredRecoird,
                data = employees.ToList()
            };

            return Ok(response);
        }
    }
}
