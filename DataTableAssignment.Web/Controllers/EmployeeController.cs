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
using DataTableAssignment.Web.Models.Response;
using AutoMapper;

namespace DataTableAssignment.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly IEmployeeService employeeService;
        private readonly DataTableAssignmentDbContext dbContext;
        private readonly IMapper mapper;
        public EmployeeController(DataTableAssignmentDbContext dbContext,  
            IHttpClientFactory httpClientFactory, 
            IConfiguration configuration,
            IEmployeeService employeeService,
            IMapper mapper)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.dbContext = dbContext;
            this.employeeService = employeeService;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployeeList(EmployeeListRequestModel requestData)
        {
            var result = await employeeService.GetFilteredEmployeesAsync(requestData);
            var response = mapper.Map<EmployeeFilterResponseModel>(result);
            response.draw = requestData.Draw;

            

            return Ok(response);
        }
    }
}
