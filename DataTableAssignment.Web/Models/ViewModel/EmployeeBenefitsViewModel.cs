using System.ComponentModel.DataAnnotations;

namespace DataTableAssignment.Web.Models.ViewModel
{
    public class EmployeeBenefitsViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string BenefitType { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Benefit value cannot be negative")]
        public int BenefitValue { get; set; }

    }
}
