using UserApplication.Controllers;
using UserApplication.Dtos;
using UserApplication.Fixture;
using UserApplication.Theory;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Xunit;
using System;
using FluentAssertions;
using AutoMapper;
using Moq;
using System.Collections.Generic;
using UserApplication.Entities.TestDb;
using UserApplication.Services;
using UsersApplication.Tests.Mock.Entities;
using UserApplication.Helpers;
using UsersApplication.Tests.Core.Orderer;
using Xunit.Abstractions;

namespace UserApplication.Tests.UnitTests
{
    /*
     Facts are tests which are always true. They test invariant conditions. 
     Theories are tests which are only true for a particular set of data
     */
    [TestCaseOrderer("PriorityOrderer", "UsersApplication.Tests")]
    public class UserControllerTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public UserControllerTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact, TestPriority(1)]
        public void UserController_GetUsers_ReturnsOkResult()
        {
            _testOutputHelper.WriteLine("Priority 1");
            //Arrange
            var _testDbContext = DbContextMock.GetMock<Users, TestDbContext>(new List<Users>() {
                new Users(){
                    Email = "testuser@test.com",
                    CreateDate = DateTime.Now,
                    FullName = "test user",
                    Id = 1,
                    Status = true
                }
            }, x => x.Users);//this object is always fresh created for each test in local context
            var config = new MapperConfiguration(cfg => cfg.AddProfile<UsersMappingProfile>());
            var _mapper = config.CreateMapper();
            var _userServiceMock = new Mock<UserService>(_testDbContext, _mapper);
            var userController = new UserController(_userServiceMock.Object);

            //Act
            var result = userController.GetUsers();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().StatusCode.Should().Be(200);
        }

        [Theory, TestPriority(2)]
        [InlineData(0)]
        public void UserController_GetUser_ReturnsActionResult(int id)
        {
            _testOutputHelper.WriteLine("Priority 2");
            //Arrange
            var _testDbContext = DbContextMock.GetMock<Users, TestDbContext>(new List<Users>(), x => x.Users);//this object is always fresh created for each test in local context
            var config = new MapperConfiguration(cfg => cfg.AddProfile<UsersMappingProfile>());
            var _mapper = config.CreateMapper();
            var _userServiceMock = new Mock<UserService>(_testDbContext, _mapper);
            var userController = new UserController(_userServiceMock.Object);

            //Act
            var result = userController.GetUser(id) as BadRequestObjectResult;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().StatusCode.Should().Be(400);
        }

        [Theory, TestPriority(4)]
        [ClassData(typeof(UserTheoryDataValid))]
        public void UserController_AddUser_ReturnsActionResult(UserDto.User userInfo)
        {
            _testOutputHelper.WriteLine("Priority 4");
            //Arrange
            var _testDbContext = DbContextMock.GetMock<Users, TestDbContext>(new List<Users>(), x => x.Users);//this object is always fresh created for each test in local context
            var config = new MapperConfiguration(cfg => cfg.AddProfile<UsersMappingProfile>());
            var _mapper = config.CreateMapper();
            var _userServiceMock = new Mock<UserService>(_testDbContext, _mapper);
            var userController = new UserController(_userServiceMock.Object);

            //Act
            var result = userController.AddUser(userInfo) as OkObjectResult;

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().StatusCode.Should().Be(200);
            userInfo.Email.Should().BeEquivalentTo(((UserDto.User)result.Value).Email);
        }

        [Theory, TestPriority(3)]
        [InlineData(1)]
        public void UserController_Delete_ReturnsActionResult(int id)
        {
            _testOutputHelper.WriteLine("Priority 3");
            //Arrange
            var _testDbContext = DbContextMock.GetMock<Users, TestDbContext>(new List<Users>(), x => x.Users);//this object is always fresh created for each test in local context
            var config = new MapperConfiguration(cfg => cfg.AddProfile<UsersMappingProfile>());
            var _mapper = config.CreateMapper();
            var _userServiceMock = new Mock<UserService>(_testDbContext, _mapper);
            var userController = new UserController(_userServiceMock.Object);

            //Act
            var result = userController.Delete(id) as BadRequestObjectResult;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().StatusCode.Should().Be(400);
        }
    }
}
