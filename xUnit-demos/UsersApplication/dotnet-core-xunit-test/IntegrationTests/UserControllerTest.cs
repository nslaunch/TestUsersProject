using UserApplication.Controllers;
using UserApplication.Dtos;
using UserApplication.Fixture;
using UserApplication.Theory;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Xunit;
using System;
using FluentAssertions;

namespace UserApplication.Tests.IntegrationTests
{
    // OneTimeSetup 
    /** https://xunit.net/docs/shared-context */
    /*
     Facts are tests which are always true. They test invariant(always true) conditions. 
     Theories are tests which are only true for a particular set of data
     */
    public class UserControllerTest : IClassFixture<ControllerFixture>
    {
        UserController userController;

        /**
         * xUnit constructor runs before each test. 
         */
        public UserControllerTest(ControllerFixture fixture)
        {
            userController = fixture.userController;
        }

        [Fact]
        public void Get_WithoutParam_Ok_Test()
        {
            var result = userController.Get() as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
            Assert.True((result.Value as string[]).Length == 1);
        }

        [Theory]
        [InlineData(0)]
        public void GetUser_WithNonUser_ThenBadRequest_Test(int id)
        {
            var result = userController.GetUser(id) as BadRequestObjectResult;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal("User not found!", result.Value);
        }

        [Theory]
        [InlineData(2)]
        public void GetUser_WithTestData_ThenOk_Test(int id)
        {
            var result = userController.GetUser(id) as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
            //Assert.IsType<UserDto.User>(result.Value);
            result.Value.Should().BeOfType<UserDto.User>();
        }

        [Theory]
        [ClassData(typeof(UserTheoryDataValid))]
        public void AddUser_WithTestData_ThenOk_Test(UserDto.User userInfo)
        {
            var result = userController.AddUser(userInfo) as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
            Assert.Equal(JsonConvert.SerializeObject(userInfo), JsonConvert.SerializeObject(result.Value));
        }

        [Theory]
        [InlineData(0)]
        public void Delete_WithNonUser_ThenBadRequest_Test(int id)
        {
            var result = userController.Delete(id) as BadRequestObjectResult;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Failed to delete user!", result.Value);
        }

        [Theory]
        [InlineData(1)]
        public void Delete_WithTestData_ThenOk_Test(int id)
        {
            var result = userController.Delete(id) as OkObjectResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<UserDto.User>(result.Value);
        }

        [Theory]
        [ClassData(typeof(UserTheoryDataInvalidEmail))]
        public void AddUser_WithInvalidEmail_ThenBadRequest_Test(UserDto.User userInfo)
        {
            var result = userController.AddUser(userInfo) as BadRequestObjectResult;
            Assert.Equal(400, result.StatusCode);
            Assert.Contains("Invalid email address!", Convert.ToString(result.Value));
        }

        [Theory]
        [ClassData(typeof(UserTheoryDataInvalidName))]
        public void AddUser_WithInvalidName_ThenBadRequest_Test(UserDto.User userInfo)
        {
            var result = userController.AddUser(userInfo) as BadRequestObjectResult;
            Assert.Equal(400, result.StatusCode);
            Assert.Contains("Full name is required!", Convert.ToString(result.Value));
        }
    }
}
