using EmployeeAPI.Controllers;
using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;

namespace EmployeeAPI.Tests
{
    //[Collection("Employee collection")]
    public class EmployeesControllerTests: IDisposable
    {
        private readonly EmployeesController _controller;
        private readonly EmployeeContext _context;
        private readonly ITestOutputHelper _testOutputHelper;

        public EmployeesControllerTests(ITestOutputHelper testOutputHelper)
        {
            var configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

            var options = new DbContextOptionsBuilder<EmployeeContext>()
                .UseSqlServer(configuration.GetConnectionString("EmployeeDatabase"))
                .Options;

            _context = new EmployeeContext(options);
            _controller = new EmployeesController(_context);
            _testOutputHelper = testOutputHelper;
            _testOutputHelper.WriteLine("In EmployeesControllerTests constructor");
        }

        [Fact]
        public void GetEmployees_ReturnsAllEmployees()
        {
            _testOutputHelper.WriteLine("executing GetEmployees_ReturnsAllEmployees");
            // Arrange
            _context.Employee.Add(new Employee { Id = 7, Name = "John Doe", Position = "Developer", Salary = 60000 });
            _context.Employee.Add(new Employee { Id = 8, Name = "Jane Doe", Position = "Manager", Salary = 80000 });
            _context.SaveChanges();

            // Act
            var result = _controller.GetEmployees();

            // Assert
            var employees = Assert.IsType<List<Employee>>(result.Value);
            Assert.Equal(2, employees.Count);
        }

        [Fact]
        public void GetEmployee_ReturnsEmployeeById()
        {
            _testOutputHelper.WriteLine("executing GetEmployee_ReturnsEmployeeById");
            // Arrange
            var employee = new Employee { Id = 9, Name = "John Doe", Position = "Developer", Salary = 60000 };
            _context.Employee.Add(employee);
            _context.SaveChanges();

            // Act
            var result = _controller.GetEmployee(9);

            // Assert
            var returnedEmployee = Assert.IsType<Employee>(result.Value);
            Assert.Equal(employee.Id, returnedEmployee.Id);
            Assert.Equal(employee.Name, returnedEmployee.Name);
        }

        [Fact]
        public void PostEmployee_AddsNewEmployee()
        {
            _testOutputHelper.WriteLine("executing PostEmployee_AddsNewEmployee");
            // Arrange
            var employee = new Employee { Id =10, Name = "John Benny", Position = "Senior Developer", Salary = 600000 };

            // Act
            var result = _controller.PostEmployee(employee);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedEmployee = Assert.IsType<Employee>(createdAtActionResult.Value);
            Assert.Equal(employee.Id, returnedEmployee.Id);
            Assert.Equal(employee.Name, returnedEmployee.Name);
            Assert.Equal(employee.Position, returnedEmployee.Position);
            Assert.Equal(employee.Salary, returnedEmployee.Salary);
        }


        [Fact]
        public void PutEmployee_UpdateEmployeeData()
        {
            _testOutputHelper.WriteLine("executing PutEmployee_UpdateEmployeeData");
            // Arrange
            var employee = new Employee { Id = 11, Name = "John Benny", Position = "Senior Developer", Salary = 600000 };
            _context.AddEmployee(employee);
            
            var updateEmployee = new Employee { Id = 11, Name = "John Benny1", Position = "Senior Developer1", Salary = 6000 };

            // Act
            var result = _controller.PutEmployee(11, updateEmployee);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteEmployee_DeleteEmployeeData()
        {
            _testOutputHelper.WriteLine("executing DeleteEmployee_DeleteEmployeeData");
            // Arrange
            var employee = new Employee { Id = 12, Name = "John Benny", Position = "Senior Developer", Salary = 600000 };
            _context.AddEmployee(employee);
            //_context.SaveChanges();

            // Act
            var result = _controller.DeleteEmployee(12);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        public void Dispose()
        {
            _testOutputHelper.WriteLine("executing Dispose, Deleting employee data");
            _context.DeleteAllEmployeeData();

            _context.Dispose();

        }
    }
}
