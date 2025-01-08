namespace DataTableAssignment.Web.Models.Enitites
{
    public class EmployeeWithDetailsAndBenefits
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public DateTime? EmployeeCreatedOn { get; set; }
        public Guid? EmployeeDetailsId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? EmployeeDetailsCreatedOn { get; set; }
        public string EmployeeBenefits { get; set; } // JSON string
    }
}


