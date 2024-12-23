using AutoMapper;
using DataTableAssignment.API.Data;
using DataTableAssignment.API.Models.Dto;
using DataTableAssignment.API.Models.Domain;
using DataTableAssignment.API.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataTableAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataTableAssignmentDbContext dbContext;
        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;
        public EmployeeController(DataTableAssignmentDbContext dbContext, IEmployeeService employeeService, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.employeeService = employeeService;
            this.mapper = mapper;

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
