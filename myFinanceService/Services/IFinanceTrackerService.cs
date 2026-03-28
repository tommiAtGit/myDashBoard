using myFinanceService.Model;


namespace myFinanceService.Services
{
    public interface IFinanceTrackerService
    {
        IEnumerable<Finance> GetAllTransactions();
        Finance GetTransactionById(Guid id);
        IEnumerable<Finance> GetTransactionsByDate(String startDate, String endDate);

        IEnumerable<Finance> GetTransactionsByAccount(string account);
        Finance AddTransaction(Finance newTransaction);

        Finance UpdateTransaction(Guid Id, Finance newTransaction);

        bool DeleteTransaction(Guid Id);


    }
}
