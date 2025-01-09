using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataTableAssignment.Web.Models.Entities
{
    public class EmployeeBenefits
    {
        [Key]
        [JsonPropertyName("BenefitId")]
        public Guid Id { get; set; }
        [JsonPropertyName("EmployeeDetailsId")]
        [Required]
        public Guid EmployeeDetailId { get; set; }

        [ForeignKey("EmployeeDetailId")]
        public EmployeeDetails EmployeeDetails { get; set; }

        [MaxLength(100)]
        public string BenefitType { get; set; }

        public int BenefitValue { get; set; }
        [JsonPropertyName("BenefitCreatedOn")]
        public DateTime? CreatedOn { get; set; }
    }
}
