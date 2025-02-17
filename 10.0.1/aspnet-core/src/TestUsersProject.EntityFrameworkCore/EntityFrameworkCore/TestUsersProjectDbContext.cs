using Abp.Zero.EntityFrameworkCore;
using TestUsersProject.Authorization.Roles;
using TestUsersProject.Authorization.Users;
using TestUsersProject.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace TestUsersProject.EntityFrameworkCore;

public class TestUsersProjectDbContext : AbpZeroDbContext<Tenant, Role, User, TestUsersProjectDbContext>
{
    /* Define a DbSet for each entity of the application */
    public virtual DbSet<Employee> Employees { get; set; }
    public TestUsersProjectDbContext(DbContextOptions<TestUsersProjectDbContext> options)
        : base(options)
    {
    }

    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseInMemoryDatabase("InMemoryDatabase");//System.Guid.NewGuid().ToString()
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
    */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("((1))");
        });

        base.OnModelCreating(modelBuilder);
    }
}
