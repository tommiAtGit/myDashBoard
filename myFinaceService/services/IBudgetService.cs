using myFinanceService.Model;

namespace myFinanceService.Services
{
    public interface IBudgetService
    {

        public Budget GetBudgetByAccount(string account);
        public Budget UpdateAccountBudget(string account, Budget budget);
        public Budget GetBudgetById(Guid Id);
        public Budget AddBudget(Budget newBudget);
        public bool DeleteBudget(Guid Id);
        public List<Budget> GetAccountBudgetHistory(string Account, DateTime startDate, DateTime endDate);


    }
}
