using myFinanceService.domain;
using System.Collections.Generic;

namespace myFinanceService.Services{
    public interface IFinanceTrackerService{
        IEnumerable<FinanceDTO>GetAllTransactions();
        FinanceDTO GetTransactionById(String id);
         IEnumerable<FinanceDTO> getTransactionsByDate(String startDate, String endDate);
         FinanceDTO addTransaction( FinanceDTO newTransaction);

         FinanceDTO updateTransaction( String Id, FinanceDTO newTransaction);

         bool deleteTransaction(String Id);


    }
}
