using System.Net.Http.Headers;
using System.Runtime.ExceptionServices;
using myFinanceService.Domain;
using myFinanceService.Services;
using NuGet.Frameworks;

namespace myFinanceService.Tests.Services
{

    public class BalanceServiceTest
    {
        private const string FIRST_ACCOUNT = "FI2180000012345678";
        private const string SECOND_ACCOUNT = "FI3080010012345690";
        private const int NUMBER_OF_TRANSACTIONS = 12;

        private IBalanceService _balanceService;

        public BalanceServiceTest()
        {
            _balanceService = new BalanceService();

        }
        [Fact]
        public void AddBalanceTest()
        {
            // Given
            FinanceDTO fianceAction = CreateNewMocDepositTransaction();

            // When
            BalanceDTO newBalance = _balanceService.AddNewBalance(fianceAction);
            // Then
            Assert.NotNull(newBalance);
            IEnumerable<BalanceDTO> balances = _balanceService.GetAllBalances();
            Assert.Single(balances);

        }
        [Fact]
        public void AddBalance_NullAction()
        {
            // Given
            FinanceDTO? fianceAction = null;
            // When

            // Then
            try
            {
                BalanceDTO newBalance = _balanceService.AddNewBalance(fianceAction);
            }
            catch (ArgumentNullException e)
            {
                Assert.True(true);
            }
            Assert.False(false);

        }
        [Fact]
        public void GetBalanceTest()
        {
            // Given
            List<FinanceDTO> financeActions = CreateMultipleFinanceActions();
            foreach (FinanceDTO f in financeActions)
            {
                BalanceDTO b = _balanceService.AddNewBalance(f);
                Assert.NotNull(b);
            }
            // When
            var firstBalance = _balanceService.GetBalance(FIRST_ACCOUNT);
            var secondBalance = _balanceService.GetBalance(SECOND_ACCOUNT);
            // Then

            Assert.NotNull(firstBalance);
            Assert.NotNull(secondBalance);
            Assert.Equal(FIRST_ACCOUNT, firstBalance.Account);
            Assert.Equal(SECOND_ACCOUNT, secondBalance.Account);



        }
        [Fact]
        public void GetBalanceWithoutAccount()
        {
            // Given
            List<FinanceDTO> financeActions = CreateMultipleFinanceActions();
            foreach (FinanceDTO f in financeActions)
            {
                BalanceDTO b = _balanceService.AddNewBalance(f);
                Assert.NotNull(b);
            }
            // When
            try
            {
                string? account = null;
                var firstBalance = _balanceService.GetBalance(account);
                Assert.False(false);
            }
            catch (ArgumentException)
            {
                Assert.True(true);
            }



        }
        [Fact]
        public void UpdateBalance()
        {
            // Given
            List<FinanceDTO> financeActions = CreateMultipleFinanceActions();
            foreach (FinanceDTO f in financeActions)
            {
                BalanceDTO b = _balanceService.AddNewBalance(f);
                Assert.NotNull(b);
            }
            // When
            FinanceDTO u = financeActions.ElementAt(3);
            u.Amount = 200.30;
            u.ActionDate = DateTime.Now;
            u.Description = "Update test";
            u.Type = ActionType.DEPOSIT;
            _balanceService.UpdateBalance(u.Account, u);
            // Then
            BalanceDTO testBalance = _balanceService.GetBalance(u.Account);
            Assert.NotNull(testBalance);
            Assert.Equal(testBalance.Account, u.Account);
            Assert.Equal(400.30, testBalance.Balance);




        }
        
        [Fact]
        public void UpdateBalance_WithNullOrEmpty_Account()
        {
            // Given

            // When
            FinanceDTO finance = new();
            var ex1 = Assert.Throws<ArgumentException>(() => _balanceService.UpdateBalance("", finance));
           
            var ex2 = Assert.Throws<ArgumentException>(() => _balanceService.UpdateBalance(null, finance));
            
            // Then
             Assert.Equal("Applied account was null or empty. (Parameter 'account')", ex1.Message);
             Assert.Equal("Applied account was null or empty. (Parameter 'account')", ex2.Message);


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

        private static List<FinanceDTO> CreateMultipleFinanceActions()
        {
            List<FinanceDTO> financeActions = new();
            for (int i = 0; i < NUMBER_OF_TRANSACTIONS; i++)
            {
                FinanceDTO dto = new FinanceDTO();
                if (i % 3 == 0)
                    dto.Type = ActionType.WITHDRAWAL;
                else
                    dto.Type = ActionType.DEPOSIT;
                if (i % 2 == 0)
                    dto.Account = FIRST_ACCOUNT;
                else
                    dto.Account = SECOND_ACCOUNT;
                dto.Description = "Save single withdrawal to my Account";
                dto.Amount = 200;
                dto.ActionDate = DateTime.Now;
                financeActions.Add(dto);
            }

            return financeActions;
        }


        private BalanceDTO CreateNewBalanceAction()
        {
            BalanceDTO balance = new();

            balance.Account = FIRST_ACCOUNT;
            balance.Balance = 235;
            balance.BalanceDate = DateTime.Today;

            return balance;
        }

    }
}