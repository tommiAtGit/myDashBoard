using myFinanceService.Domain;


namespace myFinanceService.Services
{
    public interface IFinanceTrackerService
    {
        IEnumerable<FinanceDTO> GetAllTransactions();
        FinanceDTO GetTransactionById(Guid id);
        IEnumerable<FinanceDTO> GetTransactionsByDate(String startDate, String endDate);

        IEnumerable<FinanceDTO> GetTransactionsByAccount(string account);
        FinanceDTO AddTransaction(FinanceDTO newTransaction);

        FinanceDTO UpdateTransaction(Guid Id, FinanceDTO newTransaction);

        bool DeleteTransaction(Guid Id);


    }
}
