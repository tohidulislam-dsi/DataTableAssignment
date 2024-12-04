using DataTableAssignment.Web.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Web;
using Microsoft.Extensions.Configuration;
using DataTableAssignment.Web.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Buffers;

namespace DataTableAssignment.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly DataTableAssignmentDbContext dbContext;
        public EmployeeController(DataTableAssignmentDbContext dbContext,  IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult GetEmployeeList()
        {
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string globalSearchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];
            List<EmployeeDto> response = new List<EmployeeDto>();

            var empList = dbContext.Employees.ToList();
            int totalrows = empList.Count;

            //Global search filter
            if (!string.IsNullOrEmpty(globalSearchValue))
            {
                empList = empList.
                Where(x => x.Name.ToLower().Contains(globalSearchValue.ToLower()) || x.Position.ToLower().Contains(globalSearchValue.ToLower()) || x.Office.ToLower().Contains(globalSearchValue.ToLower()) || x.Age.ToString().Contains(globalSearchValue.ToLower()) || x.Salary.ToString().Contains(globalSearchValue.ToLower())).ToList();
            }

            //Per Column filter
            if (!string.IsNullOrEmpty(Request.Form["columns[0][search][value]"]))
                empList = empList.Where(x => x.Name.ToLower().Contains(Request.Form["columns[0][search][value]"].ToString().ToLower())).ToList();
            if (!string.IsNullOrEmpty(Request.Form["columns[1][search][value]"]))
                empList = empList.Where(x => x.Position.ToLower().Contains(Request.Form["columns[1][search][value]"].ToString().ToLower())).ToList();
            if (!string.IsNullOrEmpty(Request.Form["columns[2][search][value]"]))
                empList = empList.Where(x => x.Office.ToLower().Contains(Request.Form["columns[2][search][value]"].ToString().ToLower())).ToList();
            if (!string.IsNullOrEmpty(Request.Form["columns[3][search][value]"]))
                empList = empList.Where(x => x.Age.ToString().Contains(Request.Form["columns[3][search][value]"].ToString().ToLower())).ToList();
            if (!string.IsNullOrEmpty(Request.Form["columns[4][search][value]"]))
                empList = empList.Where(x => x.Salary.ToString().Contains(Request.Form["columns[4][search][value]"].ToString().ToLower())).ToList();

            int totalrowsafterfiltering = empList.Count;
            //sorting
            empList = empList.AsQueryable().OrderBy(sortColumnName + " " + sortDirection).ToList();

            //paging
            empList = empList.Skip(start).Take(length).ToList();

             
            var jsonResponse = new JsonResult(new { data = empList, draw = Request.Form["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering });
            return jsonResponse;
        }
    }
}
