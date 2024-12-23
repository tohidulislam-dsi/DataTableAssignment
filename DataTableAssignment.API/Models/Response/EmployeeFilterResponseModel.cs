using DataTableAssignment.API.Models.Dto;
using DataTableAssignment.API.Models.ViewModel;

namespace DataTableAssignment.API.Models.Response
{
    public class EmployeeFilterResponseModel : EmployeeFilterResultDto<EmployeeViewModel>
    {
        public int draw { get; set; }
    }
}

