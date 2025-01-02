using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataTableAssignment.Web.Models.Entities
{
    public class EmployeeDetails
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        public EmployeeBenefits EmployeeBenefits { get; set; }
        public DateTime? CreatedOn { get; set; }

    }
}
