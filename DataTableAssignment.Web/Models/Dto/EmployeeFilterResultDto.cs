namespace DataTableAssignment.Web.Models.Dto
{
    public class EmployeeFilterResultDto
    {
        public IEnumerable<EmployeeDto> Employees { get; set; }
        public int TotalRecords { get; set; }
        public int TotalFilteredRecords { get; set; }
    }
}
