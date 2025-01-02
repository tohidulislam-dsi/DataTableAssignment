namespace DataTableAssignment.Web.Models.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public DateTime? CreatedOn { get; set; }
        public EmployeeDetails? EmployeeDetails { get; set; }

    }
}
