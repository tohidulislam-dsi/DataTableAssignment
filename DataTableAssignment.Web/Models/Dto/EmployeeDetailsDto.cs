using System;

namespace DataTableAssignment.Web.Models.Dto
{
    public class EmployeeDetailsDto
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<EmployeeBenefitsDto> EmployeeBenefitsDto { get; set; }

    }
}
