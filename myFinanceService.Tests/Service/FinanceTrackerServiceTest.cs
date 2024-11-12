using myFinanceService.Services;
using myFinanceService.Domain;
using myFinanceService.Model;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualBasic;
using Xunit.Sdk;
using System.Security.Principal;
using AutoMapper;
using myFinanceService.Mapper;

namespace myFinanceService.Tests.Services
{
    public class FinanceTrackerServiceTest
    {

        private const int NUMBER_OF_TRANSACTIONS = 5;
        private const string FIRST_ACCOUNT = "FI2180000012345678";
        private const string SECOND_ACCOUNT = "FI3080010012345690";

        private IFinanceTrackerService _financeTracker;

        private IMapper _mapper;

        public FinanceTrackerServiceTest()
        {
            // Set up AutoMapper with the same profile as in the main project
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>(); // Reuse your main profile
            });

            _mapper = config.CreateMapper();

            _financeTracker = new FinanceTrackerService(_mapper);
        }

        [Fact]
        public void AddTransactionTest()
        {
            // Given
            Finance dto = CreateNewMocDepositTransaction();

            // When
            Finance transaction = _financeTracker.AddTransaction(dto);
            // Then
            Assert.NotNull(transaction);
            Assert.True(transaction.Id != Guid.Empty);
            Assert.Equal(ActionType.DEPOSIT, transaction.Type);


        }
        [Fact]
        public void AddTransactionNullTest()
        {
            // Given
            Finance? dto = null;
            // When

            try
            {
                Finance transaction = _financeTracker.AddTransaction(dto);
            }
            catch (ArgumentNullException e)
            {
                Assert.True(true);
            }
            Assert.False(false);



        }
        [Fact]
        public void GetTransactionsTest()
        {
            // Given
            List<Finance> actions = CreateNewMocTransactionsWithDifferentAccount();
            foreach (Finance f in actions)
            {
                var result = _financeTracker.AddTransaction(f);
                if (result == null)
                {
                    throw new Exception();
                }

            }
            // When
            IEnumerable<Finance> transActions = _financeTracker.GetAllTransactions();
            // Then
            Assert.NotNull(transActions);
            Assert.Equal(20*2, transActions.Count());
        }
        [Fact]
        public void GetTransactionByIdTest()
        {
            // Given
            List<Finance> actions = CreateNewMocWithdrawalTransactions();
            foreach (Finance f in actions)
            {
                var result = _financeTracker.AddTransaction(f);
                if (result == null)
                {
                    throw new Exception();
                }

            }

            IEnumerable<Finance> savedActions = _financeTracker.GetAllTransactions();

            var savedTransActionId = savedActions.First().Id;

            // When

            var transAction = _financeTracker.GetTransactionById(savedTransActionId);

            // Then

            Assert.NotNull(transAction);
            Assert.Equal(savedActions.First().Id, transAction.Id);
            Assert.Equal(savedActions.First().Account, transAction.Account);
            Assert.Equal(savedActions.First().ActionDate, transAction.ActionDate);


        }
        [Fact]
        public void GetTransactionByDateTest()
        {
            // Given
             List<Finance> actions = CreateNewMocWithdrawalTransactions();
            foreach (Finance f in actions)
            {
                var result = _financeTracker.AddTransaction(f);
                if (result == null)
                {
                    throw new Exception();
                }
            }

            // When
             var transAction = _financeTracker.GetTransactionsByDate(DateAndTime.Now.ToString(), DateTime.Now.AddDays(-2).ToString());

            // Then
            Assert.NotNull(transAction);
            Assert.Equal(7,transAction.Count() );
        }

        [Fact]
        public void GetTransactionsByAccount()
        {
            // Given
            List<Finance> actions = CreateNewMocTransactionsWithDifferentAccount();
            foreach (Finance f in actions)
            {
                var result = _financeTracker.AddTransaction(f);
                if (result == null)
                {
                    throw new Exception();
                }

            }

            // When
            var results = _financeTracker.GetTransactionsByAccount(FIRST_ACCOUNT);

            // Then
            Assert.NotNull(results);
            Assert.Equal(10*2, results.Count());
            Assert.Equal(FIRST_ACCOUNT, results.First().Account);
        }

        [Fact]
        public void GetTransactionsByAccount_NullAndEmptyAccount()
        {
            // Given
            List<Finance> actions = CreateNewMocTransactionsWithDifferentAccount();
            foreach (Finance f in actions)
            {
                var result = _financeTracker.AddTransaction(f);
                if (result == null)
                {
                    throw new Exception();
                }

            }

            // When
            try
            {
                string account = "";
                var results = _financeTracker.GetTransactionsByAccount(account);
            }
            catch (ArgumentException ex)
            {
                Assert.True(true);
            }
            Assert.False(false);

        }

        [Fact]
        public void UpdateTransactionTest()
        {
            // Given
            List<Finance> actions = CreateNewMocTransactionsWithDifferentAccount();
            foreach (Finance f in actions)
            {
                var result = _financeTracker.AddTransaction(f);
                if (result == null)
                {
                    throw new Exception();
                }

            }
            // When
            IEnumerable<Finance> savedActions = _financeTracker.GetAllTransactions();

            var savedTransActionId = savedActions.First().Id;


            var action = CreateNewMocDepositTransaction();
            _financeTracker.UpdateTransaction(savedTransActionId, action);
            // Then
            var testAction = _financeTracker.GetTransactionById(savedTransActionId);
            Assert.NotNull(testAction);
            Assert.Equal(action.Description, testAction.Description);

        }
        [Fact]
        public void DeleteTransactionTest()
        {
            // Given
            List<Finance> actions = CreateNewMocTransactionsWithDifferentAccount();
            foreach (Finance f in actions)
            {
                var result = _financeTracker.AddTransaction(f);
                if (result == null)
                {
                    throw new Exception();
                }

            }
            // When
            IEnumerable<Finance> deleteActions = [];
            deleteActions = _financeTracker.GetAllTransactions();
            //var deleteActions = _financeTracker.GetAllTransactions();
            Guid id = deleteActions.ElementAt(3).Id;
            var deleteResult = _financeTracker.DeleteTransaction(id);
            Assert.True(deleteResult);
            // Then
            IEnumerable<Finance> testActions = [];
            testActions = _financeTracker.GetAllTransactions();
            Assert.NotEqual(actions.Count, testActions.Count());
           
        }

        private static Finance CreateNewMocDepositTransaction()
        {
            Finance dto = new Finance();
            dto.Type = ActionType.DEPOSIT;
            dto.Account = FIRST_ACCOUNT;
            dto.Description = "Save single deposit to my Account";
            dto.Amount = 200;
            dto.ActionDate = DateTime.Now;
            return dto;
        }
        private static Finance CreateNewMocWithdrawalTransaction()
        {
            Finance dto = new Finance();
            dto.Type = ActionType.WITHDRAWAL;
            dto.Account = FIRST_ACCOUNT;
            dto.Description = "Save single withdrawal to my Account";
            dto.Amount = 200;
            dto.ActionDate = DateTime.Now;
            return dto;
        }
        private static List<Finance> CreateNewMocWithdrawalTransactions()
        {

            List<Finance> transactions = new();
            for (int i = 0; i < NUMBER_OF_TRANSACTIONS; i++)
            {

                Finance dto = new Finance();
                dto.Id = Guid.NewGuid();
                dto.Type = ActionType.WITHDRAWAL;
                dto.Account = FIRST_ACCOUNT;
                dto.Description = "Save to my Account_action#" + i;
                dto.Amount = 2.5 * i;
                dto.ActionDate = DateTime.Now.AddDays(i*(-1));
                transactions.Add(dto);

            }

            return transactions;
            ;
        }
        private List<Finance> CreateNewMocTransactionsWithDifferentAccount()
        {

            List<Finance> transactions = new();
            for (int i = 0; i < NUMBER_OF_TRANSACTIONS; i++)
            {

                Finance dto = new Finance();
                dto.Id = Guid.NewGuid();
                dto.Type = ActionType.WITHDRAWAL;
                dto.Account = FIRST_ACCOUNT;
                dto.Description = "Save to my Account_action#" + i + ".0";
                dto.Amount = 3 * i;
                dto.ActionDate = DateTime.Now;
                transactions.Add(dto);

                Finance dto_a = new Finance();
                dto.Id = Guid.NewGuid();
                dto_a.Type = ActionType.DEPOSIT;
                dto_a.Account = FIRST_ACCOUNT;
                dto_a.Description = "Save to my Account_action#" + i + ".1";
                dto_a.Amount = 10 * i;
                dto_a.ActionDate = DateTime.Now;
                transactions.Add(dto_a);

                Finance dto_b = new Finance();
                dto.Id = Guid.NewGuid();
                dto_b.Type = ActionType.WITHDRAWAL;
                dto_b.Account = SECOND_ACCOUNT;
                dto_b.Description = "Save to my Account_action#" + i + ".2";
                dto_b.Amount = 3 * i;
                dto_b.ActionDate = DateTime.Now;
                transactions.Add(dto_b);

                Finance dto_c = new Finance();
                dto.Id = Guid.NewGuid();
                dto_c.Type = ActionType.DEPOSIT;
                dto_c.Account = SECOND_ACCOUNT;
                dto_c.Description = "Save to my Account_action#" + i + ".3";
                dto_c.Amount = 10 * i;
                dto_c.ActionDate = DateTime.Now;
                transactions.Add(dto_c);

            }

            return transactions;

        }

    }
}