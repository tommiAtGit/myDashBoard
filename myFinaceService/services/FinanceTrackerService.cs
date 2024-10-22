using myFinanceService.Domain;
using myFinanceService.Repository;
using System.Collections.Generic;
using System.Globalization;

namespace myFinanceService.Services
{
    public class FinanceTrackerService : IFinanceTrackerService
    {
        private IMockFinanceTrackerRepository _repository;

        public FinanceTrackerService()
        {
            _repository = new MockFinanceTrackerRepository();
        }
        FinanceDTO IFinanceTrackerService.AddTransaction(FinanceDTO newTransaction)
        {
            if (newTransaction != null)

                return _repository.AddNewTransaction(newTransaction);
            else
            {
                throw new ArgumentNullException();
            }
        }

        IEnumerable<FinanceDTO> IFinanceTrackerService.GetAllTransactions()
        {
            return _repository.GetAllTransactions();
        }

        FinanceDTO IFinanceTrackerService.GetTransactionById(Guid id)
        {
            return _repository.GetTransactionById(id);
        }

        IEnumerable<FinanceDTO> IFinanceTrackerService.GetTransactionsByDate(string startDate, string endDate)
        {
            var cultureInfo = new CultureInfo("fi-FI");

            DateTime dateStart = DateTime.Parse(startDate, cultureInfo);
            var start = dateStart;
            DateTime dateEnd = DateTime.Parse(endDate, cultureInfo);
            var end = dateEnd;

            return _repository.GetTransactionByDate(start, end);
        }

        FinanceDTO IFinanceTrackerService.UpdateTransaction(Guid Id, FinanceDTO newTransaction)
        {
            return _repository.UpdateTransaction(Id, newTransaction);
        }
        public bool DeleteTransaction(Guid Id)
        {
            return _repository.DeleteTransaction(Id);
        }

        public IEnumerable<FinanceDTO> GetTransactionsByAccount(string account)
        {
           if((account == null) || (account == "")){
            throw new ArgumentException(nameof(account), "Argument null or empty");
           }
           return _repository.GetTransactionsByAccount(account);
        }
    }
}