using myFinanceService.Model;

namespace myFinanceService.Services{
    public class BudgetService : IBudgetService
    {
        public Budget AddBudget(Budget newBudget)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBudget(Guid Id)
        {
            throw new NotImplementedException();
        }

        public List<Budget> GetAccountBudgetHistory(string Account, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Budget GetBudgetByAccount(string account)
        {
            throw new NotImplementedException();
        }

        public Budget GetBudgetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Budget UpdateAccountBudget(string account, Budget budget)
        {
            throw new NotImplementedException();
        }
    }
}