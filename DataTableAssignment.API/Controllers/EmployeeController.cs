using AutoMapper;
using DataTableAssignment.API.Data;
using DataTableAssignment.API.Repositories;
using DataTableAssignment.API.Models.Dto;
using DataTableAssignment.API.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataTableAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataTableAssignmentDbContext dbContext;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;
        public EmployeeController(DataTableAssignmentDbContext dbContext, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employeeDomain = await employeeRepository.GetEmployeesAsync();

            var employeeDto = mapper.Map<IEnumerable<EmployeeDto>>(employeeDomain);
            //string[] employeeNames = new string[] { "John", "Doe", "Jane", "Doe" };
            return Ok(employeeDto);
        }
    }
}
