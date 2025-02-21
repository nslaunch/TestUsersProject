
using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Data
    {
        public class EmployeeContext : DbContext
        {
            public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

            public DbSet<EmployeeAPI.Models.Employee> Employee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json")
                     .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("EmployeeDatabase"));
            }
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public void AddEmployee(EmployeeAPI.Models.Employee employee)
        {
            using (var context = new EmployeeContext(new DbContextOptions<EmployeeContext>()))
            {
                context.Employee.Add(employee);
                context.SaveChanges();
            }
        }

        public void DeleteAllEmployeeData()
        {
            using (var context = new EmployeeContext(new DbContextOptions<EmployeeContext>()))
            {
                context.Employee.ExecuteDelete();
                context.SaveChanges();
            }
        }
    }
    }