using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataTableAssignment.Web.Models.Entities
{
    public class EmployeeDetails
    {
        [Key]
        [JsonPropertyName("EmployeeDetailsId")]
        public Guid Id { get; set; }

        [Required]
        [JsonPropertyName("EmployeeId")]
        public Guid EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        public ICollection<EmployeeBenefits> EmployeeBenefits { get; set; } = new List<EmployeeBenefits>();

        [JsonPropertyName("EmployeeDetailsCreatedOn")]
        public DateTime? CreatedOn { get; set; }
    }
}
