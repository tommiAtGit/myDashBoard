using myFinanceService.Domain;

namespace myFinanceService.Repository
{

       public interface IMockBudgetRepository
       {
              public IEnumerable<BudgetDTO>GetAllBudgets();
              public IEnumerable<BudgetDTO> GetBudgetByAccount(string account);
              public BudgetDTO UpdateBudget(Guid id, BudgetDTO budget);
              public BudgetDTO GetBudgetById(Guid id);
              public BudgetDTO AddBudget(BudgetDTO newBudget);
              public bool DeleteBudget(Guid id);
            
       }
}
