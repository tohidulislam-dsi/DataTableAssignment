using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataTableAssignment.Web.Models.Entities
{
    public class EmployeeBenefits
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid EmployeeDetailId { get; set; }

        [ForeignKey("EmployeeDetailId")]
        public EmployeeDetails EmployeeDetails { get; set; }

        [MaxLength(100)]
        public string BenefitType { get; set; }

        public int BenefitValue { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
