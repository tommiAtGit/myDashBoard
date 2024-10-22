using myFinanceService.Domain;
using System.Collections.Generic;


namespace myFinanceService.Repository{
    public interface IMockFinanceTrackerRepository{
        public IEnumerable<FinanceDTO> GetAllTransactions();
        public FinanceDTO GetTransactionById(Guid id);
        public IEnumerable<FinanceDTO> GetTransactionByDate(DateTime startTime, DateTime EndTime);
        public IEnumerable<FinanceDTO> GetTransactionsByAccount(string AccountId);
        public FinanceDTO AddNewTransaction(FinanceDTO financeDTO);
        public FinanceDTO UpdateTransaction(Guid Id, FinanceDTO financeDTO);
        public bool DeleteTransaction(Guid Id); 
        

    }
}