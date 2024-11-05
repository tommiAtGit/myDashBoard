using Microsoft.AspNetCore.Mvc;
using myFinanceService.Domain;

namespace myFinanceService.Repository
{
    public class MockBudgetRepository : IMockBudgetRepository
    {
        private List<BudgetDTO> _repository;
        private const string FIRST_ACCOUNT = "FI2180000012345678";
        private const string SECOND_ACCOUNT = "FI3080010012345690";

        public MockBudgetRepository()
        {
            _repository = [];
            mockContent();


        }
        public IEnumerable<BudgetDTO> GetAllBudgets()
        {
            return _repository;
        }

        public BudgetDTO AddBudget(BudgetDTO newBudget)
        {
            if (newBudget == null)
                throw new ArgumentException(nameof(newBudget), "Argument was null");
            newBudget.Id = Guid.NewGuid();
            _repository.Add(newBudget);
            return newBudget;
        }

        public bool DeleteBudget(Guid Id)
        {
            BudgetDTO? dto = null;
            try
            {
                dto = GetBudgetById(Id);
                return _repository.Remove(dto);
            }
            catch (Exception)
            {
                return false;
            }

        }

        public IEnumerable<BudgetDTO> GetBudgetByAccount(string account)
        {

            if (account == null)
                throw new ArgumentException(nameof(account), "Argument was null");

            IEnumerable<BudgetDTO>? dto = _repository.FindAll(a => a.BudgetAccount == account) ?? throw new Exception();
            return dto;
        }

        public BudgetDTO GetBudgetById(Guid Id)
        {
            if (Id == Guid.Empty)
                throw new ArgumentException(nameof(Id), "Argument was null");
            BudgetDTO? dto = _repository.Find(a => a.Id == Id);
            if (dto == null)
            {
                throw new Exception();
            }
            return dto;
        }

        public BudgetDTO UpdateBudget(Guid Id, BudgetDTO budget)
        {
            if (Id == Guid.Empty)
                throw new ArgumentException(nameof(Id), "Argument was null");
            BudgetDTO b = GetBudgetById(Id);
            int index = _repository.IndexOf(b);

            b.BudgetAccount = budget.BudgetAccount;
            b.BudgetEndDate = budget.BudgetEndDate;
            b.BudgetStartDate = budget.BudgetStartDate;
            b.BudgetTitle = budget.BudgetTitle;
            b.BudgetValue = budget.BudgetValue;

            _repository.Insert(index, b);
            return b;


        }

        private void mockContent()
        {
            List<BudgetDTO> mockContent = [];

            for (int i = 0; i < 10; i++)
            {
                BudgetDTO dto = new()
                {
                    Id = Guid.NewGuid(),
                    BudgetTitle = "Test budget_#" + i,
                    BudgetValue = 10000,
                    BudgetEndDate = DateTime.Now.AddDays(-10),
                    BudgetStartDate = DateTime.Now
                };
                if (i % 2 == 0)
                    dto.BudgetAccount = FIRST_ACCOUNT;
                else
                    dto.BudgetAccount = SECOND_ACCOUNT;

                _repository.Add(dto);

            }


        }
    }
}