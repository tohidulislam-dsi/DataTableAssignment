using Microsoft.EntityFrameworkCore;
using DataTableAssignment.Web.Models.Domain;
namespace DataTableAssignment.Web.Data
{
    public class DataTableAssignmentDbContext: DbContext
    {
        public DataTableAssignmentDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeWithTotalFilteredRecords> EmployeeWithTotalFilteredRecords { get; set; }
    }
}
