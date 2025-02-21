using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UserApplication.Controllers;
using Xunit;

namespace UserApplication.Tests.UnitTests
{
    public class HomeControllerTest
    {
        [Fact]
        public void HomeController_Index_ReturnsViewResult()
        { 
            //Arrange
            var iLoggerMock = new Mock<ILogger<HomeController>>();
            var homeController = new HomeController(iLoggerMock.Object);

            //Act
            var result = homeController.Index();

            //Assert
            result.Should().BeOfType<ViewResult>();
        }
    }
}
