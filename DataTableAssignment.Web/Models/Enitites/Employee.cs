using System.Text.Json.Serialization;

namespace DataTableAssignment.Web.Models.Entities
{
    public class Employee
    {
        [JsonPropertyName("EmployeeId")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        [JsonPropertyName("EmployeeCreatedOn")]
        public DateTime? CreatedOn { get; set; }
        public EmployeeDetails? EmployeeDetails { get; set; }

    }
}
