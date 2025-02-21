using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeAPI.Controllers;
using EmployeeAPI.Data;
using EmployeeAPI.Models;
using EmployeeAPI.Tests.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmployeeAPI.Tests
{
    [Collection("Employee collection")]
    public class MultipleCollectionTests
    {
        private readonly EmployeesController _controller;
        private readonly EmployeeContext _context;

        private readonly EmployeeFixture fixture;

        public MultipleCollectionTests(EmployeeFixture fixture)
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

        [Fact, TestPriority(1)]
        public void Test1_GetEmployees_ReturnsAllEmployees()
        {
            // Arrange
            _context.Employee.Add(new Employee { Id = 7, Name = "John Doe", Position = "Developer", Salary = 60000 });
            _context.Employee.Add(new Employee { Id = 8, Name = "Jane Doe", Position = "Manager", Salary = 80000 });
            _context.SaveChanges();

            // Act
            var result = _controller.GetEmployees();

            // Assert
            var employees = Assert.IsType<List<Employee>>(result.Value);
            //Assert.Equal(7, employees.Count);
            Assert.IsType<List<Employee>>(employees);
        }

        [Fact, TestPriority(2)]
        public void Test2_GetEmployee_ReturnsEmployeeById()
        {
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

        [Fact, TestPriority(3)]
        public void Test3_PostEmployee_AddsNewEmployee()
        {
            // Arrange
            var employee = new Employee { Id = 10, Name = "John Benny", Position = "Senior Developer", Salary = 600000 };

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
    }
}
