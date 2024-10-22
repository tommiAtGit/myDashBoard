using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using myFinanceService.controllers;
using myFinanceService.Domain;
using myFinanceService.Services;
using myFinanceService.TestUtils;
using Microsoft.AspNetCore.Http.HttpResults;


namespace myFinanceService.Tests.Controllers
{
    public class FinanceTrackerControllerTest
    {
        
        private readonly Mock<IFinanceTrackerService> _mockService;
        private readonly FinanceTrackerController _controller;

        private readonly FinanceServiceTestUtils testUtils; 

        public FinanceTrackerControllerTest()
        {
            
            _mockService = new Mock<IFinanceTrackerService>();
            _controller = new FinanceTrackerController(_mockService.Object);

            testUtils = new();
        }

        [Fact]
        public void GetAllFinanceActionsTest()
        {
            // Given
            _mockService.Setup(service => service.GetAllTransactions()).Returns(testUtils.CreateNewMocTransactionsWithDifferentAccount());
            // When

            var result = _controller.GetAllTransactions();
            // Then

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedActions = Assert.IsAssignableFrom<IEnumerable<FinanceDTO>>(okResult.Value);
            Assert.Equal( testUtils.GetNumberOfTransactions()* 4, returnedActions?.Count());
        }

        [Fact]
        public void GetFinanceActionByIdTest_ReturnsOKResult_WithObject()
        {
            // Given
            Guid id = Guid.NewGuid();
            FinanceDTO finance = testUtils.CreateNewMocDepositTransaction(id);
            _mockService.Setup(service => service.GetTransactionById(id)).Returns(finance);
            // When
            var result = _controller.GetTransaction(id);

            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAction = Assert.IsType<FinanceDTO>(okResult.Value);
            Assert.Equal(finance.Id, returnedAction.Id);
            Assert.Equal(finance.Type, returnedAction.Type);
            Assert.Equal(testUtils.GetFirstAccount(), returnedAction.Account);
            Assert.Equal(finance.Amount, returnedAction.Amount);


        }
        [Fact]
        public void GetFinanceActionByIdTest_ReturnsNotFoundResult()
        {
            // Given
            Guid id = Guid.NewGuid();
            FinanceDTO finance = testUtils.CreateNewMocDepositTransaction(id);
            _mockService.Setup(service => service.GetTransactionById(Guid.NewGuid())).Returns(finance);
            // When
            var result = _controller.GetTransaction(id);

            // Then
            Assert.IsType<NotFoundResult>(result.Result);


        }

        [Fact]
        public void GetFinanceActionByDateTest_ReturnsOKResult_WithListOfObject()
        {
            // Given
            List<FinanceDTO> finances = testUtils.CreateNewMocTransactionsWithDifferentAccount();
            string startDate = DateTime.Now.AddDays(-3).ToString();
            string endDate = DateTime.Now.AddDays(-1).ToString();

            _mockService.Setup(service => service.GetTransactionsByDate(startDate, endDate)).Returns(finances);
            // When
            var results = _controller.GetTransactionByDate(startDate, endDate);
            // Then
            var okResult = Assert.IsType<OkObjectResult>(results.Result);
            var returnedFinanceAction = Assert.IsAssignableFrom<IEnumerable<FinanceDTO>>(okResult.Value);
            Assert.Equal(testUtils.GetNumberOfTransactions() * 4, returnedFinanceAction?.Count());
        }

        [Fact]
        public void GetFinanceActionByDateTest_ReturnsNotFoundResult()
        {
           // Given
            List<FinanceDTO> finances = testUtils.CreateNewMocTransactionsWithDifferentAccount();
            string startDate = DateTime.Now.AddDays(-3).ToString();
            string endDate = DateTime.Now.AddDays(-1).ToString();

            _mockService.Setup(service => service.GetTransactionsByDate(startDate, endDate)).Returns(finances);
            // When
            var results = _controller.GetTransactionByDate("startDate", "endDate");
            // Then
            Assert.IsType<NotFoundResult>(results.Result);
        }


        [Fact]
        public void AddNewFinanceActionTest()
        {
            // Given
            Guid id = Guid.NewGuid();
            FinanceDTO newFinance = testUtils.CreateNewMocDepositTransaction(id);
            _mockService.Setup(service => service.AddTransaction(newFinance)).Returns(newFinance);

            // Act
            var result = _controller.AddTransaction(newFinance);

            // Assert
            var okResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedAction = Assert.IsType<FinanceDTO>(okResult.Value);
            Assert.Equal(newFinance.Id, returnedAction.Id);
            Assert.Equal(newFinance.Account, returnedAction.Account);



        }

        [Fact]
        public void UpdateFinanceAction_ReturnsOkResult_WhenProductIsUpdated()
        {
            // Given
            Guid id = Guid.NewGuid();
            FinanceDTO updateFinance =testUtils.CreateNewMocDepositTransaction(id);
            _mockService.Setup(service => service.UpdateTransaction(id, updateFinance)).Returns(updateFinance);

            // When
            var result = _controller.UpdateTransaction(id, updateFinance);

            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProduct = Assert.IsType<FinanceDTO>(okResult.Value);
            Assert.Equal(updateFinance.Id, returnedProduct.Id);
            Assert.Equal(updateFinance.Account, returnedProduct.Account);

        }
        [Fact]
        public void UpdateProduct__ReturnsNotFoundResult()
        {
            // Given
            Guid id = Guid.NewGuid();
            FinanceDTO updateFinance = testUtils.CreateNewMocDepositTransaction(id);
            _mockService.Setup(service => service.UpdateTransaction(Guid.NewGuid(), updateFinance)).Returns(updateFinance);

            // When
            var result = _controller.UpdateTransaction(id, updateFinance);

            // Then
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void DeleteProduct_ReturnsNoContent_WhenProductIsDeleted()
        {
            // Given
            Guid id = Guid.NewGuid();
            _mockService.Setup(service => service.DeleteTransaction(id)).Returns(true);

            // When
            var result = _controller.DeleteTransaction(id);

            // Then
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Given
            Guid id = Guid.NewGuid();
            _mockService.Setup(service => service.DeleteTransaction(id)).Returns(false);

            // When
            var result = _controller.DeleteTransaction(id);

            // Then
            Assert.IsType<NotFoundResult>(result);


        }
     
       

    }
}