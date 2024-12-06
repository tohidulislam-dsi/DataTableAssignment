using DataTableAssignment.Web.Models.Dto;

namespace DataTableAssignment.Web.Models.Response
{
    public class EmployeeFilterResponseModel: EmployeeFilterResultDto<EmployeeDto>
    {
        public int draw { get; set; }
    }
}

