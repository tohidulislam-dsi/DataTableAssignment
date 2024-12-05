using DataTableAssignment.Web.Models.Domain;

namespace DataTableAssignment.Web.Models.Dto
{
    public class FilteredEmployeeDto
    {
        public IEnumerable<Employee> Employees { get; set; }
        public int TotalFilteredRecords { get; set; }

    }
}
