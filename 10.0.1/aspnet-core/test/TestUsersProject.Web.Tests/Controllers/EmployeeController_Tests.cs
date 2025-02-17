using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using TestUsersProject.Tests.Core.Mock;
using TestUsersProject.Tests.Core.TheoryData;
using TestUsersProject.Users.Dto;
using TestUsersProject.Web.Controllers;
using Xunit.Abstractions;
using Xunit;
using TestUsersProject.Web.Helpers;
using TestUsersProject.Tests;
using TestUsersProject.EntityFrameworkCore;

namespace TestUsersProject.Web.Tests.Controllers
{
    public class EmployeeController_Tests : TestUsersProjectTestBase
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private EmployeeService _employeeServiceMock;
        public EmployeeController_Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _employeeServiceMock = Resolve<EmployeeService>();
        }

        [Fact]
        public void employeeController_GetUsers_ReturnsOkResult()
        {
            //Arrange
            var _testDbContext = Resolve<TestUsersProjectDbContext>();
            _testDbContext.Employees.AddRange(new List<Employee>() {
                new Employee(){
                    Email = "testuser@test.com",
                    CreateDate = DateTime.Now,
                    FullName = "test user",
                    Id = 1,
                    Status = true
                }
            });
            _testDbContext.SaveChanges();
            var expectedCount = _testDbContext.Employees.Count();
            //var config = new MapperConfiguration(cfg => cfg.AddProfile<EmployeeMappingProfile>());
            //var _mapper = config.CreateMapper();
            //var _employeeServiceMock = Resolve<EmployeeService>();
            var employeeController = new EmployeeController(_employeeServiceMock);

            //Act
            var result = employeeController.GetEmployees();

            //Assert
            result.ShouldBeOfType<OkObjectResult>();
            result.As<OkObjectResult>().StatusCode.ShouldBe(200);
            result.As<OkObjectResult>().Value.ShouldBeOfType<List<EmployeeDto>>();
            result.As<OkObjectResult>().Value.As<List<EmployeeDto>>().Count.ShouldBe(expectedCount);
        }

        [Theory]
        [InlineData(0)]
        public void employeeController_GetUser_ReturnsActionResult(int id)
        {
            //Arrange
            //var _testDbContext = DbContextMock.GetMock<Employee, TestUsersProjectDbContext>(new List<Employee>(), x => x.Employees);//this object is always fresh created for each test in local context
            //var config = new MapperConfiguration(cfg => cfg.AddProfile<EmployeeMappingProfile>());
            //var _mapper = config.CreateMapper();
            //var _employeeServiceMock = Resolve<EmployeeService>();
            var employeeController = new EmployeeController(_employeeServiceMock);

            //Act
            var result = employeeController.GetUser(id) as BadRequestObjectResult;

            //Assert
            result.ShouldBeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().StatusCode.ShouldBe(400);
        }

        [Theory]
        [ClassData(typeof(EmployeeTheoryDataValid))]
        public void employeeController_AddUser_ReturnsActionResult(EmployeeDto userInfo)
        {
            //Arrange
            //var _testDbContext = DbContextMock.GetMock<Employee, TestUsersProjectDbContext>(new List<Employee>(), x => x.Employees);//this object is always fresh created for each test in local context
            //var config = new MapperConfiguration(cfg => cfg.AddProfile<EmployeeMappingProfile>());
            //var _mapper = config.CreateMapper();
            //var _employeeServiceMock = Resolve<EmployeeService>();
            var employeeController = new EmployeeController(_employeeServiceMock);

            //Act
            var result = employeeController.Add(userInfo) as OkObjectResult;

            //Assert
            result.ShouldBeOfType<OkObjectResult>();
            result.As<OkObjectResult>().StatusCode.ShouldBe(200);
            userInfo.Email.ShouldBeEquivalentTo(((EmployeeDto)result.Value).Email);
        }

        [Theory]
        [InlineData(1)]
        public void employeeController_Delete_ReturnsActionResult(int id)
        {
            //Arrange
            //var _testDbContext = DbContextMock.GetMock<Employee, TestUsersProjectDbContext>(new List<Employee>(), x => x.Employees);//this object is always fresh created for each test in local context
            //var config = new MapperConfiguration(cfg => cfg.AddProfile<EmployeeMappingProfile>());
            //var _mapper = config.CreateMapper();
            //var _employeeServiceMock = Resolve<EmployeeService>();
            var employeeController = new EmployeeController(_employeeServiceMock);

            //Act
            var result = employeeController.Delete(id) as BadRequestObjectResult;

            //Assert
            result.ShouldBeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().StatusCode.ShouldBe(400);
        }

        [Theory]
        [InlineData(1)]
        public void employeeController_DeleteSuccess_ReturnsActionResult(int id)
        {
            //Arrange
            var _testDbContext = Resolve<TestUsersProjectDbContext>();
            _testDbContext.Employees.AddRange(new List<Employee>() {
                new Employee(){
                    Email = "testuser@test.com",
                    CreateDate = DateTime.Now,
                    FullName = "test user",
                    Id = 1,
                    Status = true
                }
            });
            _testDbContext.SaveChanges();
            //var _testDbContext = DbContextMock.GetMock<Employee, TestUsersProjectDbContext>(new List<Employee>(), x => x.Employees);//this object is always fresh created for each test in local context
            //var config = new MapperConfiguration(cfg => cfg.AddProfile<EmployeeMappingProfile>());
            //var _mapper = config.CreateMapper();
            //var _employeeServiceMock = Resolve<EmployeeService>();
            var employeeController = new EmployeeController(_employeeServiceMock);

            //Act
            var result = employeeController.Delete(id) as OkObjectResult;

            //Assert
            result.ShouldBeOfType<OkObjectResult>();
            result.As<OkObjectResult>().StatusCode.ShouldBe(200);
        }

        [Fact]
        public async Task employeeController_LongRunning_ReturnsString()
        {
            //Arrange
            //var _employeeServiceMock = Resolve<EmployeeService>();
            var employeeController = new EmployeeController(_employeeServiceMock);

            //Act 
            var result = await employeeController.LongRunning();

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<ContentResult>();
        }
    }
}
