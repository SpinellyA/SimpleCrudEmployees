
using Microsoft.EntityFrameworkCore;
public class MyDbContext : DbContext
{

    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeStatus> EmployeeStatuses { get; set; }
    public DbSet<EmployeeStatusType> EmployeeStatusTypes {get;set; }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeStatus>().HasOne(e => e.Employee).WithMany().HasForeignKey(e => e.EmployeeId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EmployeeStatus>().HasOne(es => es.EmployeeStatusType).WithMany().HasForeignKey(e => e.EmployeeStatusTypeId);



        modelBuilder.Entity<EmployeeStatusType>().HasData(
            new EmployeeStatusType { Id = 1, Name = "Active", Description = "Currently active, has a paycheck." },
            new EmployeeStatusType { Id = 2, Name = "Inactive", Description = "Not active, not working, not getting paid." },
            new EmployeeStatusType { Id = 3, Name = "Draft", Description = "Draft, unfinished."},
            new EmployeeStatusType { Id = 4, Name = "Deprecated", Description = "Blacklisted, deleted, not working." }
            );

    }

}