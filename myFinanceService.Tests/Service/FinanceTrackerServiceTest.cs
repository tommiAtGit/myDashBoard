using myFinanceService.Services;
using myFinanceService.Domain;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualBasic;
using Xunit.Sdk;
using System.Security.Principal;

namespace myFinanceService.Tests.Services
{
    public class FinanceTrackerServiceTest
    {

        private const int NUMBER_OF_TRANSACTIONS = 5;
        private const string FIRST_ACCOUNT = "FI2180000012345678";
        private const string SECOND_ACCOUNT = "FI3080010012345690";

        private IFinanceTrackerService _financeTracker;

        public FinanceTrackerServiceTest()
        {
            _financeTracker = new FinanceTrackerService();
        }

        [Fact]
        public void AddTransactionTest()
        {
            // Given
            FinanceDTO dto = CreateNewMocDepositTransaction();

            // When
            FinanceDTO transaction = _financeTracker.AddTransaction(dto);
            // Then
            Assert.NotNull(transaction);
            Assert.True(transaction.Id != Guid.Empty);
            Assert.Equal(ActionType.DEPOSIT, transaction.Type);


        }
        [Fact]
        public void AddTransactionNullTest(){
            // Given
            FinanceDTO? dto = null;
            // When

            try{
                FinanceDTO transaction = _financeTracker.AddTransaction(dto);
            }
            catch(ArgumentNullException e){
                Assert.True(true);
            }
            Assert.False(false);
            
            

        }
        [Fact]
        public void GetTransactionsTest()
        {
            // Given
            List<FinanceDTO> actions = CreateNewMocTransactionsWithDifferentAccount();
            foreach (FinanceDTO f in actions)
            {
                var result = _financeTracker.AddTransaction(f);
                if (result == null)
                {
                    throw new Exception();
                }

            }
            // When
            IEnumerable<FinanceDTO> transActions = _financeTracker.GetAllTransactions();
            // Then
            Assert.NotNull(transActions);
            Assert.Equal(20, transActions.Count());
        }
        [Fact]
        public void GetTransactionByIdTest()
        {
            // Given
            List<FinanceDTO> actions = CreateNewMocWithdrawalTransactions();
            foreach (FinanceDTO f in actions)
            {
                var result = _financeTracker.AddTransaction(f);
                if (result == null)
                {
                    throw new Exception();
                }

            }
            var transActionId = actions[1].Id;

            // When

            var transAction = _financeTracker.GetTransactionById(transActionId);

            // Then

            Assert.NotNull(transAction);
            Assert.Equal(actions[1].Id, transAction.Id);
            Assert.Equal(actions[1].Account, transAction.Account);
            Assert.Equal(actions[1].ActionDate, transAction.ActionDate);


        }
        [Fact]
        public void GetTransactionByDateTest()
        {
            // Given

            // When

            // Then
        }

        [Fact]
        public void GetTransactionsByAccount()
        {
            // Given
            List<FinanceDTO> actions = CreateNewMocTransactionsWithDifferentAccount();
            foreach (FinanceDTO f in actions)
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
            Assert.Equal(10, results.Count());
            Assert.Equal(FIRST_ACCOUNT, results.First().Account);
        }

         [Fact]
        public void GetTransactionsByAccount_NullAndEmptyAccount()
        {
            // Given
            List<FinanceDTO> actions = CreateNewMocTransactionsWithDifferentAccount();
            foreach (FinanceDTO f in actions)
            {
                var result = _financeTracker.AddTransaction(f);
                if (result == null)
                {
                    throw new Exception();
                }

            }

            // When
            try{
                string account = "";
                var results = _financeTracker.GetTransactionsByAccount(account);
            }
            catch (ArgumentException ex){
                Assert.True(true);
            }
            Assert.False(false);
            
        }

        [Fact]
        public void UpdateTransactionTest()
        {
            // Given
            List<FinanceDTO> actions = CreateNewMocTransactionsWithDifferentAccount();
            foreach (FinanceDTO f in actions)
            {
                var result = _financeTracker.AddTransaction(f);
                if (result == null)
                {
                    throw new Exception();
                }

            }
            // When
            var Id = actions[2].Id;

            var action = CreateNewMocDepositTransaction();
            _financeTracker.UpdateTransaction(Id, action);
            // Then
            var testAction = _financeTracker.GetTransactionById(Id);
            Assert.NotNull(testAction);
            Assert.Equal(action.Description, testAction.Description);

        }
        [Fact]
        public void DeleteTransactionTest()
        {
            // Given
            List<FinanceDTO> actions = CreateNewMocTransactionsWithDifferentAccount();
            foreach (FinanceDTO f in actions)
            {
                var result = _financeTracker.AddTransaction(f);
                if (result == null)
                {
                    throw new Exception();
                }

            }
            // When
            IEnumerable<FinanceDTO> deleteActions = [];
             deleteActions = _financeTracker.GetAllTransactions();
            //var deleteActions = _financeTracker.GetAllTransactions();
            Guid id = deleteActions.ElementAt(3).Id;
            var deleteResult = _financeTracker.DeleteTransaction(id);
            Assert.True(deleteResult);
            // Then
            IEnumerable<FinanceDTO> testActions = [];
            testActions = _financeTracker.GetAllTransactions();
             Assert.NotEqual(actions.Count, testActions.Count());
              Assert.True(testActions.Count() == actions.Count - 1 );
        }

        private static FinanceDTO CreateNewMocDepositTransaction()
        {
            FinanceDTO dto = new FinanceDTO();
            dto.Type = ActionType.DEPOSIT;
            dto.Account = FIRST_ACCOUNT;
            dto.Description = "Save single deposit to my Account";
            dto.Amount = 200;
            dto.ActionDate = DateTime.Now;
            return dto;
        }
        private static FinanceDTO CreateNewMocWithdrawalTransaction()
        {
            FinanceDTO dto = new FinanceDTO();
            dto.Type = ActionType.WITHDRAWAL;
            dto.Account = FIRST_ACCOUNT;
            dto.Description = "Save single withdrawal to my Account";
            dto.Amount = 200;
            dto.ActionDate = DateTime.Now;
            return dto;
        }
        private static List<FinanceDTO> CreateNewMocWithdrawalTransactions()
        {

            List<FinanceDTO> transactions = new();
            for (int i = 0; i < NUMBER_OF_TRANSACTIONS; i++)
            {

                FinanceDTO dto = new FinanceDTO();
                dto.Type = ActionType.WITHDRAWAL;
                dto.Account = FIRST_ACCOUNT;
                dto.Description = "Save to my Account_action#" + i;
                dto.Amount = 2.5 * i;
                dto.ActionDate = DateTime.Now;
                transactions.Add(dto);

            }

            return transactions;
            ;
        }
        private List<FinanceDTO> CreateNewMocTransactionsWithDifferentAccount()
        {

            List<FinanceDTO> transactions = new();
            for (int i = 0; i < NUMBER_OF_TRANSACTIONS; i++)
            {

                FinanceDTO dto = new FinanceDTO();
                dto.Type = ActionType.WITHDRAWAL;
                dto.Account = FIRST_ACCOUNT;
                dto.Description = "Save to my Account_action#" + i + ".0";
                dto.Amount = 3 * i;
                dto.ActionDate = DateTime.Now;
                transactions.Add(dto);

                FinanceDTO dto_a = new FinanceDTO();
                dto_a.Type = ActionType.DEPOSIT;
                dto_a.Account = FIRST_ACCOUNT;
                dto_a.Description = "Save to my Account_action#" + i + ".1";
                dto_a.Amount = 10 * i;
                dto_a.ActionDate = DateTime.Now;
                transactions.Add(dto_a);

                FinanceDTO dto_b = new FinanceDTO();
                dto_b.Type = ActionType.WITHDRAWAL;
                dto_b.Account = SECOND_ACCOUNT;
                dto_b.Description = "Save to my Account_action#" + i + ".2";
                dto_b.Amount = 3 * i;
                dto_b.ActionDate = DateTime.Now;
                transactions.Add(dto_b);

                FinanceDTO dto_c = new FinanceDTO();
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