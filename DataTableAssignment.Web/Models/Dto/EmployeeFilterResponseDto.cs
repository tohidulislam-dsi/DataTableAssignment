namespace DataTableAssignment.Web.Models.Dto
{
    public class EmployeeFilterResponseDto
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<EmployeeDto> data { get; set; }
    }
}

