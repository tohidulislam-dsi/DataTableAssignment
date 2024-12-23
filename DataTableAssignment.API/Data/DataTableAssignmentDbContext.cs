using DataTableAssignment.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataTableAssignment.API.Data
{
    public class DataTableAssignmentDbContext: DbContext
    {
        public DataTableAssignmentDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
                
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeWithTotalFilteredRecords> EmployeeWithTotalFilteredRecords { get; set; }
    }
}
