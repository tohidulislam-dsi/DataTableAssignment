namespace DataTableAssignment.Web.Models.Enitites
{
    public class EmployeeWithDetailsAndBenefits
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? EmployeeDetailsId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? EmployeeDetailsCreatedOn { get; set; }
        public Guid? EmployeeBenefitsId { get; set; }
        public string BenefitType { get; set; }
        public int BenefitValue { get; set; }
        public DateTime? EmployeeBenefitsCreatedOn { get; set; }
    }
}


