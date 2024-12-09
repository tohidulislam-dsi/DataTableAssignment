using DataTableAssignment.Web.Models.Dto;
using DataTableAssignment.Web.Models.ViewModel;

namespace DataTableAssignment.Web.Models.Response
{
    public class EmployeeFilterResponseModel: EmployeeFilterResultDto<EmployeeViewModel>
    {
        public int draw { get; set; }
    }
}

