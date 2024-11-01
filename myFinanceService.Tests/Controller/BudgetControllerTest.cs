using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using myFinanceService.controllers;
using myFinanceService.Domain;
using myFinanceService.Model;
using myFinanceService.Services;
using myFinanceService.TestUtils;
using Microsoft.AspNetCore.Http.HttpResults;

namespace myFinanceService.controllers
{
    public class BudgetControllerTest
    {
        private readonly Mock<IBudgetService> _mockService;
        private readonly BudgetController _controller;

        private readonly FinanceServiceTestUtils utils;

        public BudgetControllerTest()
        {

            _mockService = new Mock<IBudgetService>();
            _controller = new BudgetController(_mockService.Object);
            utils = new();

        }

        [Fact]
        public void GetAllBudgetsTest()
        {
            // Given
            var budgets = utils.CreateBudgetWithDifferentAccount();
            _mockService.Setup(service => service.GetAllBudgets()).Returns(budgets);
            // When
            var result = _controller.GetAllBudgets();
            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBudget = Assert.IsAssignableFrom<IEnumerable<Budget>>(okResult.Value);
            Assert.Equal(2, returnedBudget?.Count());

        }

        [Fact]
        public void GetBudgetByAccount_ReturnOkAndBudgetObject()
        {
            // Given
            var budget = utils.CreateBudget();
            _mockService.Setup(service => service.GetBudgetByAccount(utils.GetFirstAccount())).Returns(budget);
            // When
            var result = _controller.GetBudgetByAccount(utils.GetFirstAccount());
            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBudget = Assert.IsAssignableFrom<Budget>(okResult.Value);
            Assert.Equal(utils.GetFirstAccount(), returnedBudget.BudgetAccount);
            Assert.Equal(budget.BudgetAccount, returnedBudget.BudgetAccount);
            Assert.Equal(budget.BudgetValue, returnedBudget.BudgetValue);
        }

        [Fact]
        public void GetBudgetByAccount_ReturnedNotFound()
        {
            var budget = utils.CreateBudget();
            _mockService.Setup(service => service.GetBudgetByAccount(utils.GetFirstAccount())).Returns(budget);
            // When
            var result = _controller.GetBudgetByAccount(utils.GetSecondAccount());

            // Then
            Assert.IsType<NotFoundResult>(result.Result);

        }
        [Fact]
        public void UpdateBudget__ReturnOkAndBudgetObject()
        {
            // Given
            var budget = utils.CreateBudget();
            _mockService.Setup(service => service.UpdateAccountBudget(budget.Id, budget)).Returns(budget);

            // When
            var result = _controller.UpdateBudget(budget.Id, budget);

            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBudget = Assert.IsAssignableFrom<Budget>(okResult.Value);
            Assert.NotNull(returnedBudget);
            Assert.Equal(budget.Id, returnedBudget.Id);
        }
        [Fact]
        public void UpdateBudget_ReturnedBadRequest()
        {
            // Given
            var budget = utils.CreateBudget();
            _mockService.Setup(service => service.UpdateAccountBudget(budget.Id, budget)).Returns(budget);

            // When
            var result = _controller.UpdateBudget(Guid.Empty, budget);

            // Then
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public void UpdateBudget_ReturnedNotFound()
        {
            // Given
            var budget = utils.CreateBudget();
            _mockService.Setup(service => service.UpdateAccountBudget(budget.Id, budget)).Returns(budget);

            // When
            var result = _controller.UpdateBudget(Guid.NewGuid(), budget);

            // Then
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void AddNewBudget_ReturnOKResult_WithObject()
        {
            // Given
            var budget = utils.CreateBudget();
            _mockService.Setup(service => service.AddBudget(budget)).Returns(budget);

            // When
            var result = _controller.AddBudget(budget);
            // Then
            var okResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedBudget = Assert.IsType<Budget>(okResult.Value);
            Assert.NotNull(returnedBudget);
            Assert.Equal(budget.BudgetAccount, returnedBudget.BudgetAccount);
            Assert.Equal(budget.BudgetTitle, returnedBudget.BudgetTitle);
            Assert.Equal(budget.BudgetValue, returnedBudget.BudgetValue);
        }

        [Fact]
        public void AddNewBudget_ReturnsBadRequest()
        {
            var budget = utils.CreateBudget();
            _mockService.Setup(service => service.AddBudget(budget)).Returns(budget);

            Budget? b1 = null;
            // When
            var result = _controller.AddBudget(b1);

            // Then
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void DeleteProduct_ReturnsNoContent_WhenProductIsDeleted()
        {
            // Given
            Guid id = Guid.NewGuid();
            _mockService.Setup(service => service.DeleteBudget(id)).Returns(true);

            // When
            var result = _controller.DeleteBudget(id);

            // Then
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void _ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Given
            Guid id = Guid.NewGuid();
            _mockService.Setup(service => service.DeleteBudget(id)).Returns(false);

            // When
            var result = _controller.DeleteBudget(id);

            // Then
            Assert.IsType<NotFoundResult>(result);
        }


    }
}