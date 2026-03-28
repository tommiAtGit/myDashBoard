using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using myFinanceService.Domain;
using myFinanceService.Model;
using myFinanceService.Repository;

namespace myFinanceService.Services
{
    public class BalanceService : IBalanceService
    {

        private IMockBalanceRepository _balancerepo;
        private IMapper _mapper;
        public BalanceService(IMapper mapper)
        {
            _balancerepo = new MockBalanceRepository();
            _mapper = mapper;
        }
        public Balance AddNewBalance(Finance financeAction)
        {
            Balance balance = new();

            if (financeAction == null)
                throw new ArgumentNullException(nameof(financeAction), "Applied argument was null");
            balance = CalculateBalance(true,_mapper.Map<Finance>(financeAction));
            balance.Account = financeAction.Account;

            return _mapper.Map<Balance>(_balancerepo.AddNewBalance(_mapper.Map<BalanceDTO>(balance)));
        }


        public IEnumerable<Balance> GetAllBalances()
        {
            return _mapper.Map<IEnumerable<Balance>>(_balancerepo.GetAllBalances());
        }
        public Balance GetBalance(string account)
        {
            if ((account == null) || (account == ""))
                throw new ArgumentException(nameof(account), "Applied account  was null or empty");
            var balance = _balancerepo.GetBalance(account);
            if (balance == null)
            {
                throw new NullReferenceException("No balance found with account" + account);
            }
            return _mapper.Map<Balance>(balance);
        }

        public Balance UpdateBalance(String account, Finance financeAction)
        {

            if ((account == null) || (account == ""))
                throw new ArgumentException("Applied account was null or empty.", nameof(account));


            var newBalance = CalculateBalance(false,financeAction);
            return _mapper.Map<Balance>(_balancerepo.UpdateBalance(account, _mapper.Map<BalanceDTO>(newBalance)));

        }

        private Balance CalculateBalance(bool newAction, Finance financeAction)
        {
            Balance balance = new();

            if (financeAction == null)
                throw new ArgumentNullException(nameof(financeAction), "Applied argument was null");
            if (newAction)
            {
                switch (financeAction.Type)
                {
                    case ActionType.DEPOSIT:
                        balance.AccountBalance =  financeAction.Amount;
                        balance.BalanceDate = DateTime.Now;
                        break;
                    case ActionType.WITHDRAWAL:
                        balance.AccountBalance =  - financeAction.Amount;
                        balance.BalanceDate = DateTime.Now;
                        break;

                }

            }
            else
            {
                balance = GetBalance(financeAction.Account);
                switch (financeAction.Type)
                {
                    case ActionType.DEPOSIT:
                        balance.AccountBalance = balance.AccountBalance + financeAction.Amount;
                        balance.BalanceDate = DateTime.Now;
                        break;
                    case ActionType.WITHDRAWAL:
                        balance.AccountBalance = balance.AccountBalance - financeAction.Amount;
                        balance.BalanceDate = DateTime.Now;
                        break;

                }

            }

            return balance;

        }

        public bool DeleteBalance(Guid id)
        {
            if (id == Guid.Empty )
                throw new ArgumentException(nameof(id), "Applied account  was null or empty");
            
            return _balancerepo.DeleteBalance(id);
        }
    }
}
