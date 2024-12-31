using System;

namespace DataTableAssignment.Web.Models.Dto
{
    public class EmployeeBenefitsDto
    {
        public Guid Id { get; set; }
        public Guid EmployeeDetailId { get; set; }
        public string BenefitType { get; set; }
        public int BenefitValue { get; set; }
    }
}
