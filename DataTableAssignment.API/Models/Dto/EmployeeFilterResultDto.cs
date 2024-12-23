namespace DataTableAssignment.API.Models.Dto
{
    public class EmployeeFilterResultDto<T>
    {
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public IEnumerable<T> data { get; set; }
    }
}
