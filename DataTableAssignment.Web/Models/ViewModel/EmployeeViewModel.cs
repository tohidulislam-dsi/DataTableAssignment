using System.ComponentModel.DataAnnotations;
namespace DataTableAssignment.Web.Models.ViewModel
{
    public class EmployeeViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Position { get; set; }
        public string Office { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Age cannot be negative")]

        public int Age { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Salary cannot be negative")]

        public int Salary { get; set; }
    }
}
