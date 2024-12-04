using DataTableAssignment.Web.Models.Dto;

namespace DataTableAssignment.Web.Models.Response
{
    public class EmployeeFilterResponseModel
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<EmployeeDto> data { get; set; }
    }
}

