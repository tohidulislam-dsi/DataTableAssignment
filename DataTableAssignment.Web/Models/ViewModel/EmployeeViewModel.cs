using System.ComponentModel.DataAnnotations;
namespace DataTableAssignment.Web.Models.ViewModel
{
    public class EmployeeViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Position { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Office { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Age cannot be negative")]
        public int Age { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Salary cannot be negative")]
        public int Salary { get; set; }
        public EmployeeDetailViewModel EmployeeDetails { get; set; }

        public EmployeeViewModel()
        {
            EmployeeDetails = new EmployeeDetailViewModel();
        }

    }
}
