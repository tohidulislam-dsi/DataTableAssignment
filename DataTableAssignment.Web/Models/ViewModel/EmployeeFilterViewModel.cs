using DataTableAssignment.Web.Models.Dto;

namespace DataTableAssignment.Web.Models.ViewModel
{
    public class EmployeeFilterViewModel:EmployeeFilterResultDto<EmployeeViewModel>
    {
        public int draw { get; set; }
    }
}
