using Microsoft.AspNetCore.Mvc;
using Moq;
using myFinanceService.Controllers;
using myFinanceService.Model;
using myFinanceService.Services;
using Xunit;

namespace myFinanceService.Tests.Controller
{
    public class BalanceSheetControllerTest
    {
        private readonly Mock<IBalanceSheetService> _mockService;
        private readonly BalanceSheetController _controller;

        public BalanceSheetControllerTest()
        {
            _mockService = new Mock<IBalanceSheetService>();
            _controller = new BalanceSheetController(_mockService.Object);
        }

        [Fact]
        public void GetAll_ReturnsOkResult()
        {
            // Arrange
            _mockService.Setup(s => s.GetAllBalanceSheets()).Returns(new List<BalanceSheet>());

            // Act
            var result = _controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetById_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.GetBalanceSheetById(id)).Returns(new BalanceSheet { Id = id });

            // Act
            var result = _controller.GetById(id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.GetBalanceSheetById(id)).Returns(new BalanceSheet());

            // Act
            var result = _controller.GetById(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
