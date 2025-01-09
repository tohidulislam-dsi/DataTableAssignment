using System.ComponentModel.DataAnnotations;

namespace DataTableAssignment.Web.Models.ViewModel
{
    public class EmployeeDetailViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string PhoneNumber { get; set; }
        public List<EmployeeBenefitsViewModel> EmployeeBenefits { get; set; } = new List<EmployeeBenefitsViewModel>
        {
            new EmployeeBenefitsViewModel(),
            new EmployeeBenefitsViewModel(),
            new EmployeeBenefitsViewModel()
        };

    }
}
