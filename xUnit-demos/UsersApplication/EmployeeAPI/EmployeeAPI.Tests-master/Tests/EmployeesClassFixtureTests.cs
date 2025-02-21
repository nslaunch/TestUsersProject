using EmployeeAPI.Controllers;
using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmployeeAPI.Tests
{
    public class EmployeesClassFixtureTests : IClassFixture<EmployeeFixture>
    {
        private readonly EmployeesController _controller;
        private readonly EmployeeContext _context;

        private readonly EmployeeFixture fixture;

        public EmployeesClassFixtureTests(EmployeeFixture fixture)
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
            this.fixture = fixture;
        }

        [Fact]
        public void GetEmployees_ReturnsAllEmployees()
        {
            // Arrange
            _context.Employee.Add(new Employee { Id = 1, Name = "John Doe", Position = "Developer", Salary = 60000 });
            _context.Employee.Add(new Employee { Id = 2, Name = "Jane Doe", Position = "Manager", Salary = 80000 });
            _context.SaveChanges();

            // Act
            var result = _controller.GetEmployees();

            // Assert
            var employees = Assert.IsType<List<Employee>>(result.Value);
            //Assert.Equal(2, employees.Count);
        }

        [Fact]
        public void GetEmployee_ReturnsEmployeeById()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "John Doe", Position = "Developer", Salary = 60000 };
            _context.Employee.Add(employee);
            _context.SaveChanges();

            // Act
            var result = _controller.GetEmployee(1);

            // Assert
            var returnedEmployee = Assert.IsType<Employee>(result.Value);
            Assert.Equal(employee.Id, returnedEmployee.Id);
            Assert.Equal(employee.Name, returnedEmployee.Name);
        }

        [Fact]
        public void PostEmployee_AddsNewEmployee()
        {
            // Arrange
            var employee = new Employee { Id =31, Name = "John Benny", Position = "Senior Developer", Salary = 600000 };

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
            // Arrange
            var employee = new Employee { Id = 32, Name = "John Benny", Position = "Senior Developer", Salary = 600000 };
            _context.AddEmployee(employee);
            
            var updateEmployee = new Employee { Id = 32, Name = "John Benny1", Position = "Senior Developer1", Salary = 6000 };

            // Act
            var result = _controller.PutEmployee(32, updateEmployee);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteEmployee_DeleteEmployeeData()
        {
            // Arrange
            var employee = new Employee { Id = 32, Name = "John Benny", Position = "Senior Developer", Salary = 600000 };
            _context.AddEmployee(employee);
            //_context.SaveChanges();

            // Act
            var result = _controller.DeleteEmployee(32);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
