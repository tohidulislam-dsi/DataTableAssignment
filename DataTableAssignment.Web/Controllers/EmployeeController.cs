﻿using DataTableAssignment.Web.Models.Dto;
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
using DataTableAssignment.Web.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            return View("AddOrEdit", new EmployeeViewModel());

        }
        [HttpPost]
        public async Task<ActionResult> Add(EmployeeViewModel employee)
        {
            var employeeDto = mapper.Map<EmployeeDto>(employee);

            //var employeeDetailsDto = mapper.Map<EmployeeDetailsDto>(employee);
            //var employeeBenefitsDto = mapper.Map<EmployeeBenefitsDto>(employee);
            var employeeId = await employeeService.AddEmployeeAsync(employeeDto);
            //return RedirectToAction("Index");
            return Json(new OperationResult { Success = true, Message = "Saved Successfully", Id = employeeId });

        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var employee = await employeeService.GetEmployeeById(id);

            return View("AddOrEdit", employee);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(EmployeeViewModel employee)
        {
            var employeeDto = mapper.Map<EmployeeDto>(employee);
            
            //var employeeDetailsDto = mapper.Map<EmployeeDetailsDto>(employee);
            //var employeeBenefitsDto = mapper.Map<EmployeeBenefitsDto>(employee);
            await employeeService.UpdateEmployeeAsync(employeeDto);
            //return RedirectToAction("Index");

            return Json(new OperationResult { Success = true, Message = "Updated Successfully", Id = employee.Id });

        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            await employeeService.DeleteEmployeeById(id);
            return Json(new OperationResult { Success = true, Message = "Deleted Successfully", Id = id });
        }
    }
}
