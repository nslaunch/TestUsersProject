using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shouldly;
using TestUsersProject.EntityFrameworkCore;
using TestUsersProject.Tests.Core.TheoryData;
using TestUsersProject.Users;
using TestUsersProject.Users.Dto;
using Xunit;

namespace TestUsersProject.Tests.Employee
{
    public class EmployeeServiceTests : TestUsersProjectTestBase
    {
        //private readonly IEmployeeService _employeeAppService;
        private readonly IMapper _mapper;

        public EmployeeServiceTests()
        {
            //_employeeAppService = Resolve<IEmployeeService>();
            _mapper = Resolve<IMapper>();
        }

        [Theory]
        [ClassData(typeof(EmployeeTheoryDataValid))]
        public void EmployeeService_AddUser_ReturnsUser(EmployeeDto userInfo)
        {
            //Arrange
            var testDbContext = Resolve<TestUsersProjectDbContext>();
            testDbContext.Employees.AddRange(new List<TestUsersProject.Employee>() {
                new TestUsersProject.Employee(){
                    Email = "testuser@test.com",
                    CreateDate = DateTime.Now,
                    FullName = "test user",
                    Id = 1,
                    Status = true
                }
            });
            testDbContext.SaveChanges();
            var employeeService = Resolve<IEmployeeService>();

            //Act 
            var result = employeeService.AddEmployee(userInfo);

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeDto>();
        }

        [Fact]
        public void EmployeeService_UpdateUser_ReturnsUser()
        {
            //Arrange
            var employeeObj = new TestUsersProject.Employee()
            {
                Email = "testuser@test.com",
                CreateDate = DateTime.Now,
                FullName = "test user",
                Id = 1,
                Status = true
            };
            var testDbContext = Resolve<TestUsersProjectDbContext>();
            testDbContext.Employees.AddRange(new List<TestUsersProject.Employee>() {
                employeeObj
            });
            testDbContext.SaveChanges();
            var employeeService = Resolve<IEmployeeService>();

            //Act 
            var userInfo = _mapper.Map<EmployeeDto>(employeeObj);
            userInfo.FullName = "test user updated";
            var result = employeeService.UpdateEmployee(userInfo);

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeDto>();
            result.FullName.ShouldBe(userInfo.FullName);
        }

        [Theory]
        [ClassData(typeof(EmployeeTheoryDataInvalidName))]
        public void EmployeeService_AddUser_InvalidNameThrowsException(EmployeeDto userInfo)
        {
            //Arrange
            var employeeService = Resolve<IEmployeeService>();

            //Act             
            Action userAddAction = () => { employeeService.AddEmployee(userInfo); };

            //Assert
            userAddAction.ShouldThrow<Exception>().Message.ShouldBe("Full name is required!");
        }

        [Theory]
        [ClassData(typeof(EmployeeTheoryDataInvalidEmail))]
        public void EmployeeService_AddUser_InvalidEmailThrowsException(EmployeeDto userInfo)
        {
            //Arrange
            var employeeService = Resolve<IEmployeeService>();

            //Act 
            Action userAddAction = () => { employeeService.AddEmployee(userInfo); };

            //Assert
            userAddAction.ShouldThrow<Exception>().Message.ShouldBe("Invalid email address!");
        }

        [Theory]
        [InlineData(1)]
        public void EmployeeService_DeleteUser_ReturnsUser(int id)
        {
            //Arrange
            var testDbContext = Resolve<TestUsersProjectDbContext>();
            testDbContext.Employees.AddRange(new List<TestUsersProject.Employee>() {
                new TestUsersProject.Employee(){
                    Email = "testuser@test.com",
                    CreateDate = DateTime.Now,
                    FullName = "test user",
                    Id = 1,
                    Status = true
                }
            });
            testDbContext.SaveChanges();
            var employeeService = Resolve<IEmployeeService>();

            //Act 
            var result = employeeService.DeleteEmployee(id);

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeDto>();
        }

        [Theory]
        [InlineData(1)]
        public void EmployeeService_GetUser_ReturnsUser(int id)
        {
            //Arrange
            var testDbContext = Resolve<TestUsersProjectDbContext>();
            testDbContext.Employees.AddRange(new List<TestUsersProject.Employee>() {
                new TestUsersProject.Employee(){
                    Email = "testuser@test.com",
                    CreateDate = DateTime.Now,
                    FullName = "test user",
                    Id = 1,
                    Status = true
                }
            });
            testDbContext.SaveChanges();
            var expectedId = 1;
            var employeeService = Resolve<IEmployeeService>();

            //Act 
            var result = employeeService.GetEmployee(id);

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeDto>();
            result.Id.ShouldBe(expectedId);
        }

        [Fact]
        public void EmployeeService_GetUsers_ReturnsUsersList()
        {
            //Arrange
            var employeeService = Resolve<IEmployeeService>();

            //Act 
            var result = employeeService.GetAllEmployees();

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<EmployeeDto>>();
            //result.Count.Should().Be(2);
        }
    }
}
