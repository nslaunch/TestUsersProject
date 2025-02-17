using TestUsersProject.Models.TokenAuth;
using TestUsersProject.Web.Controllers;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace TestUsersProject.Web.Tests.Controllers;

public class HomeController_Tests : TestUsersProjectWebTestBase
{
    [Fact]
    public async Task Index_Test()
    {
        //Arrange
        var url = GetUrl<HomeController>(nameof(HomeController.Index));

        //await AuthenticateAsync(null, new AuthenticateModel
        //{
        //    UserNameOrEmailAddress = "admin",
        //    Password = "123qwe"
        //});

        //Act
        var response = await GetResponseAsStringAsync(url);

        //Assert
        response.ShouldNotBeNullOrEmpty();
        response.ShouldBeOfType<string>();
    }
}