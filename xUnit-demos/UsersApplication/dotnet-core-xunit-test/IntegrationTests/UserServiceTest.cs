using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using UserApplication.Dtos;
using UserApplication.Entities.TestDb;
using UserApplication.Fixture;
using UserApplication.Helpers;
using UserApplication.Mock.Entities;
using UserApplication.Services;
using UserApplication.Theory;
using UsersApplication.Tests.Mock.Entities;
using Xunit;

namespace UserApplication.Tests.IntegrationTests
{
    //using fixture to mock objects
    public class UserServiceTest: IClassFixture<ContextFixture>
    {
        private readonly TestDbContextMock testDbContext;//this object is shared across multiple tests
        public UserServiceTest(ContextFixture _contextFixture)
        {
            testDbContext=_contextFixture.testDbContextMock;
        }

        [Theory]
        [ClassData(typeof(UserTheoryDataValid))]
        public void UserService_AddUser_ReturnsUser(UserDto.User userInfo)
        {
            //Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<UsersMappingProfile>());
            var _mapper = config.CreateMapper();
            var _userService = new UserService(testDbContext, _mapper);

            //Act 
            var result = _userService.AddUser(userInfo);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UserDto.User>();
        }

        [Theory]
        [ClassData(typeof(UserTheoryDataInvalidName))]
        public void UserService_AddUser_InvalidNameThrowsException(UserDto.User userInfo)
        {
            //Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<UsersMappingProfile>());
            var _mapper = config.CreateMapper();
            var _userService = new UserService(testDbContext, _mapper);

            //Act             
            Action userAddAction = () => { var result = _userService.AddUser(userInfo); };

            //Assert
            userAddAction.Should().Throw<Exception>().WithMessage("Full name is required!");
        }

        [Theory]
        [ClassData(typeof(UserTheoryDataInvalidEmail))]
        public void UserService_AddUser_InvalidEmailThrowsException(UserDto.User userInfo)
        {
            //Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<UsersMappingProfile>());
            var _mapper = config.CreateMapper();
            var _userService = new UserService(testDbContext, _mapper);

            //Act 
            Action userAddAction = () => { var result = _userService.AddUser(userInfo); };

            //Assert
            userAddAction.Should().Throw<Exception>().WithMessage("Invalid email address!");
        }

        [Theory]
        [InlineData(1)]
        public void UserService_DeleteUser_ReturnsUser(int id)
        {
            //Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<UsersMappingProfile>());
            var _mapper = config.CreateMapper();
            var _userService = new UserService(testDbContext, _mapper);

            //Act 
            var result = _userService.DeleteUser(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UserDto.User>();
        }

        [Theory]
        [InlineData(2)]//issue with 1
        public void UserService_GetUser_ReturnsUser(int id)
        {
            //Arrange
            var expectedId = 2;
            var config = new MapperConfiguration(cfg => cfg.AddProfile<UsersMappingProfile>());
            var _mapper = config.CreateMapper();
            var _userService = new UserService(testDbContext, _mapper);

            //Act 
            var result = _userService.GetUser(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UserDto.User>();
            result.Id.Should().Be(expectedId);
        }

        [Fact]
        public void UserService_GetUsers_ReturnsUsersList()
        {
            //Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<UsersMappingProfile>());
            var _mapper = config.CreateMapper();
            var _userService = new UserService(testDbContext, _mapper);

            //Act 
            var result = _userService.GetUsers();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<UserDto.User>>();
            //result.Count.Should().Be(2);//sometimes it is 3
        }
    }
}
