using AutoMapper;
using myFinanceService.Mapper;
using myFinanceService.Model;
using myFinanceService.Domain;
using myFinanceService.TestUtils;

using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace myFinanceService.Services
{
    public class BudgetServiceTest
    {
        private IBudgetService _service;
        private IMapper _mapper;

        private readonly FinanceServiceTestUtils _utils;


        public BudgetServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>(); // Reuse your main profile
            });

            _mapper = config.CreateMapper();

            _service = new BudgetService(_mapper);

            _utils = new();

        }

        [Fact]
        public void AddBudgetTest()
        {
            // Given
            Budget budget = _utils.CreateBudget();

            // When
            var result = _service.AddBudget(budget);

            // Then
            Assert.NotNull(result);
            Assert.True(result.Id != Guid.Empty);
        }

        [Fact]
        public void GetAllBudgetsTest()
        {
            // Given
            Budget budget = _utils.CreateBudget();
            var result = _service.AddBudget(budget);


            // When
            var allResult = _service.GetAllBudgets();

            // Then
            Assert.NotNull(allResult);
            Assert.Equal(11, allResult.Count());
        }

        [Fact]
        public void GetBudgetByAccount()
        {
            // Given
            var allResult = _service.GetAllBudgets();
            string account = allResult.First().BudgetAccount;

            // When
            var byAccount = _service.GetBudgetByAccount(account);

            // Then
            Assert.NotNull(byAccount);
            Assert.Equal(5, byAccount.Count());
            Assert.Equal(_utils.GetFirstAccount(), byAccount.First().BudgetAccount);
        }
        [Fact]
        public void GetBudgetByAccount_EmptyAccount()
        {
            // Given
            var allResult = _service.GetAllBudgets();
            string account = allResult.First().BudgetAccount;

            // When
            try
            {
                var byAccount = _service.GetBudgetByAccount("");
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void GetBudgetById()
        {
            // Given
            var allResult = _service.GetAllBudgets();
            Guid id = allResult.First().Id;

            // When
            var byId = _service.GetBudgetById(id);

            // Then
            Assert.NotNull(byId);
            Assert.Equal(byId.BudgetAccount, allResult.First().BudgetAccount);
            Assert.Equal(byId.BudgetTitle, allResult.First().BudgetTitle);
            Assert.Equal(byId.BudgetValue, allResult.First().BudgetValue);

        }
        [Fact]
        public void GetBudgetById_EmptyId()
        {
            // Given
            var allResult = _service.GetAllBudgets();
            Guid id = allResult.First().Id;

            // When
            try
            {
                var byId = _service.GetBudgetById(Guid.Empty);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.True(true);

            }
        }

        [Fact]
        public void UpdateBudget()
        {
            var allResult = _service.GetAllBudgets();
            Guid id = allResult.First().Id;
            Budget ub = new()
            {
                Id = id,
                BudgetAccount = allResult.First().BudgetAccount,
                BudgetTitle = "Summer budget",
                BudgetValue = 2000,
                BudgetStartDate = DateTime.Now.AddDays(-1),
                BudgetEndDate = DateTime.Now.AddDays(-3)
            };

            // When
            var newBudget = _service.UpdateAccountBudget(id, ub);
            // Then
            Assert.NotNull(newBudget);
            Assert.Equal(ub.BudgetAccount, newBudget.BudgetAccount);
            Assert.Equal(ub.BudgetTitle, newBudget.BudgetTitle);
            Assert.Equal(ub.BudgetValue, newBudget.BudgetValue);


        }

        [Fact]
        public void UpdateBudget_emptyId()
        {
            var allResult = _service.GetAllBudgets();
            Guid id = allResult.First().Id;
            Budget ub = new()
            {
                Id = id,
                BudgetAccount = allResult.First().BudgetAccount,
                BudgetTitle = "Summer budget",
                BudgetValue = 2000,
                BudgetStartDate = DateTime.Now.AddDays(-1),
                BudgetEndDate = DateTime.Now.AddDays(-3)
            };

            // When
            try
            {
                var newBudget = _service.UpdateAccountBudget(Guid.Empty, ub);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

                Assert.True(true);
            }
        }

        [Fact]
        public void DeleteBudgetTest_ReturnsFound()
        {
            // Given
             var allResult = _service.GetAllBudgets();
            Guid id = allResult.First().Id;

            // When
            var result = _service.DeleteBudget(id);

            // Then
            Assert.True(result);
        }

        [Fact]
        public void  DeleteBudgetTest_IdEmpty()
        {
            // Given
             var allResult = _service.GetAllBudgets();
            Guid id = allResult.First().Id;

            // When
           try
           {
             var result = _service.DeleteBudget(Guid.Empty);
             Assert.Fail();
 
           }
           catch (ArgumentException)
           {
                Assert.True(true);
           }
            // Then
          
        }
        [Fact]
        public void DeleteBudgetTest_ReturnsNotFound()
        {
           // Given
             var allResult = _service.GetAllBudgets();
            Guid id = Guid.NewGuid();

            // When
            var result = _service.DeleteBudget(id);

            // Then
            Assert.False(result);
        }


    }
}