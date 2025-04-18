using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using myFinanceService.Domain;
using myFinanceService.Model;
using myFinanceService.Repository;
using System.Globalization;

namespace myFinanceService.Services
{
    public class FinanceTrackerService(IMapper mapper) : IFinanceTrackerService
    {
        private IMockFinanceTrackerRepository _repository = new MockFinanceTrackerRepository();
        private IMapper _mapper = mapper;

        public Finance AddTransaction(Finance newTransaction)
        {
            if (newTransaction != null)
            {
                var financeAction = _mapper.Map<FinanceDTO>(newTransaction);
                var actionDto = _repository.AddNewTransaction(financeAction);
                return _mapper.Map<Finance>(actionDto);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public IEnumerable<Finance> GetAllTransactions()
        {
            return _mapper.Map<IEnumerable<Finance>>(_repository.GetAllTransactions());
        }

        public Finance GetTransactionById(Guid id)
        {
            var finance = _repository.GetTransactionById(id);
            if(finance.Id == Guid.Empty)
                return new Finance();
            else
                return _mapper.Map<Finance>(finance);
        }

        public IEnumerable<Finance> GetTransactionsByDate(string startDate, string endDate)
        {
            var cultureInfo = new CultureInfo("fi-FI");

            DateTime dateStart = DateTime.Parse(startDate, cultureInfo);
            var start = dateStart.Date;
            DateTime dateEnd = DateTime.Parse(endDate, cultureInfo);
            var end = dateEnd.Date;

            return _mapper.Map<IEnumerable<Finance>>(_repository.GetTransactionByDate(start, end));
        }

        public Finance UpdateTransaction(Guid Id, Finance newTransaction)
        {
            var updatedAction = _repository.UpdateTransaction(Id, _mapper.Map<FinanceDTO>(newTransaction));
            if(updatedAction.Id == Guid.Empty){
             return new Finance();
            }

            return _mapper.Map<Finance>(updatedAction);
        }
        public bool DeleteTransaction(Guid Id)
        {
            return _repository.DeleteTransaction(Id);
        }

        public IEnumerable<Finance> GetTransactionsByAccount(string account)
        {
            if ((account == null) || (account == ""))
            {
                throw new ArgumentException(nameof(account), "Argument null or empty");
            }
            return _mapper.Map<IEnumerable<Finance>>(_repository.GetTransactionsByAccount(account));
        }
    }
}