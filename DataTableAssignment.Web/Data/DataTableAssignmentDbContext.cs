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
        public DbSet<EmployeeBenefits> EmployeeBenefits { get; set; }
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public DbSet<EmployeeWithTotalFilteredRecords> EmployeeWithTotalFilteredRecords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
