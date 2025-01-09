namespace DataTableAssignment.Web.Models.Dto
{
    public class EmployeeBenefitInfoDto
    {
        
        public Guid EmployeeBenefitsId { get; set; }
        public string BenefitType { get; set; }
        public int BenefitValue { get; set; }
        public DateTime? EmployeeBenefitsCreatedOn { get; set; }
        
    }
}
