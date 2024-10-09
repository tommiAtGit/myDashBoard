using myFinanceService.domain;
using System.Collections.Generic;

namespace myFinanceService.Services{
    public class FinanceTrackerService : IFinanceTrackerService
    {
        public bool deleteTransaction(string Id)
        {
            throw new NotImplementedException();
        }

        FinanceDTO IFinanceTrackerService.addTransaction(FinanceDTO newTransaction)
        {
            throw new NotImplementedException();
        }

        IEnumerable<FinanceDTO> IFinanceTrackerService.GetAllTransactions()
        {
            throw new NotImplementedException();
        }

        FinanceDTO IFinanceTrackerService.GetTransactionById(string id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<FinanceDTO> IFinanceTrackerService.getTransactionsByDate(string startDate, string endDate)
        {
            throw new NotImplementedException();
        }

        FinanceDTO IFinanceTrackerService.updateTransaction(string Id, FinanceDTO newTransaction)
        {
            throw new NotImplementedException();
        }
    }
}