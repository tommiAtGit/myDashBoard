using myFinanceService.Domain;

namespace myFinanceService.Repository
{

       public interface IMockBudgetRepository
       {
              public BudgetDTO GetBudgetByAccount(string account);
              public BudgetDTO UpdateAccountBudget(string account, BudgetDTO budget);
              public BudgetDTO GetBudgetById(Guid Id);
              public BudgetDTO AddBudget(BudgetDTO newBudget);
              public bool DeleteBudget(Guid Id);
              public List<BudgetDTO> GetAccountBudgetHistory(string Account, DateTime startDate, DateTime endDate);
       }
}
