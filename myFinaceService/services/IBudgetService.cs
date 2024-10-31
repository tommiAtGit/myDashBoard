using myFinanceService.Model;

namespace myFinanceService.Services
{
    public interface IBudgetService
    {

        public IEnumerable<Budget> GetAllBudgets();
        public Budget GetBudgetByAccount(string account);
        public Budget UpdateAccountBudget(Guid id, Budget budget);
        public Budget GetBudgetById(Guid id);
        public Budget AddBudget(Budget newBudget);
        public bool DeleteBudget(Guid id);


    }
}
