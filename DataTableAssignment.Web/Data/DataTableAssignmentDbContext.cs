using Microsoft.EntityFrameworkCore;
using DataTableAssignment.Web.Models.Entities;
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

            // Configure one-to-one relationship between Employee and EmployeeDetails
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.EmployeeDetails)
                .WithOne(ed => ed.Employee)
                .HasForeignKey<EmployeeDetails>(ed => ed.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure one-to-one relationship between EmployeeDetails and EmployeeBenefits
            modelBuilder.Entity<EmployeeDetails>()
                .HasOne(ed => ed.EmployeeBenefits)
                .WithOne(eb => eb.EmployeeDetails)
                .HasForeignKey<EmployeeBenefits>(eb => eb.EmployeeDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<EmployeeDetails>()
                .Property(ed => ed.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<EmployeeBenefits>()
                .Property(eb => eb.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

           

        }
    }
}
