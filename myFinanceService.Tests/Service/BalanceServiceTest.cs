using System.Net.Http.Headers;
using System.Runtime.ExceptionServices;
using myFinanceService.Domain;
using myFinanceService.Model;
using myFinanceService.Services;
using NuGet.Frameworks;
using AutoMapper;
using myFinanceService.Mapper;

namespace myFinanceService.Tests.Services
{

    public class BalanceServiceTest
    {
        private const string FIRST_ACCOUNT = "FI2180000012345678";
        private const string SECOND_ACCOUNT = "FI3080010012345690";
        private const int NUMBER_OF_TRANSACTIONS = 12;

        private IBalanceService _balanceService;
        private IMapper _mapper;

        public BalanceServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>(); // Reuse your main profile
            });

            _mapper = config.CreateMapper();

            _balanceService = new BalanceService(_mapper);

        }
        [Fact]
        public void AddBalanceTest()
        {
            // Given
            Finance fianceAction = CreateNewMocDepositTransaction();

            // When
            Balance newBalance = _balanceService.AddNewBalance(fianceAction);
            // Then
            Assert.NotNull(newBalance);
            IEnumerable<Balance> balances = _balanceService.GetAllBalances();
            Assert.Single(balances);

        }
        [Fact]
        public void AddBalance_NullAction()
        {
            // Given
            Finance? fianceAction = null;
            // When

            // Then
            try
            {
                Balance newBalance = _balanceService.AddNewBalance(fianceAction);
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
            List<Finance> financeActions = CreateMultipleFinanceActions();
            foreach (Finance f in financeActions)
            {
                Balance b = _balanceService.AddNewBalance(f);
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
            List<Finance> financeActions = CreateMultipleFinanceActions();
            foreach (Finance f in financeActions)
            {
                Balance b = _balanceService.AddNewBalance(f);
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
        public void UpdateBalance_UsingDepositAction()
        {
            // Given
            List<Finance> financeActions = CreateMultipleFinanceActions();
            foreach (Finance f in financeActions)
            {
                Balance b = _balanceService.AddNewBalance(f);
                Assert.NotNull(b);
            }
            // When
            Finance u = financeActions.ElementAt(3);
            u.Amount = 200.30;
            u.ActionDate = DateTime.Now;
            u.Description = "Update test";
            u.Type = ActionType.DEPOSIT;
            _balanceService.UpdateBalance(u.Account, u);
            // Then
            Balance testBalance = _balanceService.GetBalance(u.Account);
            Assert.NotNull(testBalance);
            Assert.Equal(testBalance.Account, u.Account);
            Assert.Equal(400.30, testBalance.AccountBalance);




        }
        [Fact]
        public void  UpdateBalance_UsingWithdrawalAction()
        {
            // Given
            List<Finance> financeActions = CreateMultipleFinanceActions();
            foreach (Finance f in financeActions)
            {
                Balance b = _balanceService.AddNewBalance(f);
                Assert.NotNull(b);
            }
            // When
            Finance u = financeActions.ElementAt(3);
            u.Amount = 100.30;
            u.ActionDate = DateTime.Now;
            u.Description = "Update test Using Withdrawal Action";
            u.Type = ActionType.WITHDRAWAL;
            _balanceService.UpdateBalance(u.Account, u);
            // Then
            Balance testBalance = _balanceService.GetBalance(u.Account);
            Assert.NotNull(testBalance);
            Assert.Equal(testBalance.Account, u.Account);
            Assert.Equal(99.7, testBalance.AccountBalance);

        }
        
        [Fact]
        public void UpdateBalance_WithNullOrEmpty_Account()
        {
            // Given

            // When
            Finance finance = new();
            var ex1 = Assert.Throws<ArgumentException>(() => _balanceService.UpdateBalance("", finance));
           
            var ex2 = Assert.Throws<ArgumentException>(() => _balanceService.UpdateBalance(null, finance));
            
            // Then
             Assert.Equal("Applied account was null or empty. (Parameter 'account')", ex1.Message);
            


        }
        [Fact]
        public void DeleteBalanceTest_ReturnBalance_Found()
        {
            // Given
     
            List<Finance> financeActions = CreateMultipleFinanceActions();
            foreach (Finance f in financeActions)
            {
                Balance b = _balanceService.AddNewBalance(f);
                Assert.NotNull(b);
            }
            IEnumerable<Balance> fa = _balanceService.GetAllBalances();
                Balance balance = fa.ElementAt(2);

                Guid balanceId = balance.Id;
            // When
               bool result =  _balanceService.DeleteBalance(balanceId);


            // Then
            Assert.True(result);
           
        }
        private static Finance CreateNewMocDepositTransaction()
        {
            Finance dto = new Finance
            {
                Type = ActionType.DEPOSIT,
                Account = FIRST_ACCOUNT,
                Description = "Save single deposit to my Account",
                Amount = 200,
                ActionDate = DateTime.Now
            };
            return dto;
        }

        private static List<Finance> CreateMultipleFinanceActions()
        {
            List<Finance> financeActions = new();
            for (int i = 0; i < NUMBER_OF_TRANSACTIONS; i++)
            {
                Finance dto = new Finance();
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


        private Balance CreateNewBalanceAction()
        {
            Balance balance = new();

            balance.Account = FIRST_ACCOUNT;
            balance.AccountBalance = 235;
            balance.BalanceDate = DateTime.Today;

            return balance;
        }

    }
}