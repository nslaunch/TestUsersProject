using EmployeeAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

//[assembly: AssemblyFixture(typeof(EmployeeFixture))]
namespace EmployeeAPI.Tests
{
    public class EmployeeFixture : IDisposable
    {
        private readonly EmployeeContext _context;
        public EmployeeFixture() 
        {
            Console.WriteLine("EmployeeFixture Constructor");

            var configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

            var options = new DbContextOptionsBuilder<EmployeeContext>()
                .UseSqlServer(configuration.GetConnectionString("EmployeeDatabase"))
                .Options;

            _context = new EmployeeContext(options);

        }
        public void Dispose()
        {
            Console.WriteLine("Disposing EmployeeFixture Started");

            _context.DeleteAllEmployeeData();

            _context.Dispose();
            Console.WriteLine("Disposing EmployeeFixture Ended");
        }
    }
}
