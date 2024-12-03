using DataTableAssignment.Web.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace DataTableAssignment.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        public EmployeeController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            
            return View();
        }

        public async Task<IActionResult> GetEmployeeList()
        {
            List<EmployeeDto> response = new List<EmployeeDto>();
            try
            {
                var client = httpClientFactory.CreateClient();
                var apiUrl = configuration["ApiSettings:EmployeeApiUrl"];
                var httpResponseMessage = await client.GetAsync(apiUrl);
                //var httpResponseMessage = await client.GetAsync("https://localhost:7289/api/employee");
                httpResponseMessage.EnsureSuccessStatusCode();
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<EmployeeDto>>());
                var jsonResponse = new JsonResult(new {data = response});

                return jsonResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return View();
        }
    }
}
