using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using myFinanceService.controllers;
using myFinanceService.Domain;
using myFinanceService.Services;
using myFinanceService.TestUtils;

namespace myFinanceService.Tests.Controllers
{
    public class BalanceControllerTest
    {
        private readonly Mock<IBalanceService> _mockService;
        private readonly BalanceController _controller;

        private readonly FinanceServiceTestUtils utils;

        public BalanceControllerTest()
        {

            _mockService = new Mock<IBalanceService>();
            _controller = new BalanceController(_mockService.Object);
            utils = new();


        }
        [Fact]
        public void GetAllBalancesTest()
        {
            // Given
            var balances = utils.CreateNewMocBalancesWithDifferentAccount();
            _mockService.Setup(service => service.GetAllBalances()).Returns(balances);
            // When
            var result = _controller.GetAllBalances();
            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBalance = Assert.IsAssignableFrom<IEnumerable<BalanceDTO>>(okResult.Value);
            Assert.Equal(utils.GetNumberOfBalances(), returnedBalance?.Count());

        }
        [Fact]
        public void GetBalanceByAccount_ReturnOkAndBalanceObject()
        {
            // Given
            var balancePocDto = utils.CreateBalanceWithFirstAccount();
            _mockService.Setup(service => service.GetBalance(utils.GetFirstAccount())).Returns(balancePocDto);

            // When
            var result = _controller.GetBalance(utils.GetFirstAccount());
            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBalance = Assert.IsAssignableFrom<BalanceDTO>(okResult.Value);
            Assert.Equal(utils.GetFirstAccount(), returnedBalance.Account);
            Assert.Equal(balancePocDto.AccountBalance, returnedBalance.Balance);
            Assert.Equal(balancePocDto.BalanceDate, returnedBalance.BalanceDate);

        }
        [Fact]
        public void GetBalanceByAccount_ReturnedNotFound()
        {
            // Given
            var balancePoc = utils.CreateBalanceWithFirstAccount();
            _mockService.Setup(service => service.GetBalance(utils.GetFirstAccount())).Returns(balancePoc);

            // When
            var result = _controller.GetBalance(utils.GetSecondAccount());
            // Then
            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public void AddNewBalance_ReturnOKResult_WithObject()
        {
            // Given
            var balancePocDto = utils.CreateBalanceWithFirstAccount();
            var depositPocDto = utils.CreateNewMocDepositTransaction();
            _mockService.Setup(service => service.AddNewBalance(depositPocDto)).Returns(balancePocDto);

            // When
            var result = _controller.AddBalance(depositPocDto);

            // Then
            var okResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedAction = Assert.IsType<BalanceDTO>(okResult.Value);
            
        }
        [Fact]
        public void UpdateBalance_ReturnOKAndBalanceObject()
        {
            // Given

            var transactionPocDto = utils.CreateNewMocDepositTransaction();
            var balancePocDto = utils.CreateBalanceWithFirstAccount();
            string account = transactionPocDto.Account;
            var mockBalance = _mockService.Setup(service => service.UpdateBalance(account, transactionPocDto)).Returns(balancePocDto);

            // When
            var result = _controller.UpdateBalance(account,transactionPocDto);

            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBalance = Assert.IsType<BalanceDTO>(okResult.Value);
             Assert.Equal(balancePocDto.Id, returnedBalance.Id);
            Assert.Equal(balancePocDto.Account, returnedBalance.Account);


        }
        [Fact]
        public void UpdateBalance_ReturnNotFound()
        {
            var transactionPocDto = utils.CreateNewMocDepositTransaction();
            var balancePocDto = utils.CreateBalanceWithFirstAccount();
            string differentAccount = utils.GetSecondAccount();
            var mockBalance = _mockService.Setup(service => service.UpdateBalance(differentAccount, transactionPocDto)).Returns(balancePocDto);

            // When
            var result = _controller.UpdateBalance(balancePocDto.Account,transactionPocDto);

            // Then
             var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public void DeleteProduct_ReturnsNoContent_WhenProductIsDeleted()
        {
            // Given
            Guid id = Guid.NewGuid();
            _mockService.Setup(service => service.DeleteBalance(id)).Returns(true);

            // When
            var result = _controller.DeleteBalance(id);

            // Then
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Given
            Guid id = Guid.NewGuid();
            _mockService.Setup(service => service.DeleteBalance(id)).Returns(false);

            // When
            var result = _controller.DeleteBalance(id);

            // Then
            Assert.IsType<NotFoundResult>(result);


        }


    }

}