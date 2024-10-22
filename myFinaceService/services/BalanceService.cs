using System.Reflection.Metadata.Ecma335;
using myFinanceService.Domain;
using myFinanceService.Repository;

namespace myFinanceService.Services
{
    public class BalanceService : IBalanceService
    {

        private IMockBalanceRepository _balancerepo;
        public BalanceService()
        {
            _balancerepo = new MockBalanceRepository();
        }
        public BalanceDTO AddNewBalance(FinanceDTO financeAction)
        {
            BalanceDTO balance = new();

            if (financeAction == null)
                throw new ArgumentNullException(nameof(financeAction), "Applied argument was null");
            balance = CalculateBalance(true,financeAction);
            balance.Account = financeAction.Account;

            return _balancerepo.AddNewBalance(balance);
        }


        public IEnumerable<BalanceDTO> GetAllBalances()
        {
            return _balancerepo.GetAllBalances();
        }
        public BalanceDTO GetBalance(string account)
        {
            if ((account == null) || (account == ""))
                throw new ArgumentException(nameof(account), "Applied account  was null or empty");
            var balance = _balancerepo.GetBalance(account);
            if (balance == null)
            {
                throw new NullReferenceException("No balance found with account" + account);
            }
            return balance;
        }

        public BalanceDTO UpdateBalance(String account, FinanceDTO financeAction)
        {

            if ((account == null) || (account == ""))
                throw new ArgumentException("Applied account was null or empty.", nameof(account));


            BalanceDTO newBalance = CalculateBalance(false,financeAction);
            return _balancerepo.UpdateBalance(account, newBalance);

        }

        private BalanceDTO CalculateBalance(bool newAction, FinanceDTO financeAction)
        {
            BalanceDTO balance = new();

            if (financeAction == null)
                throw new ArgumentNullException(nameof(financeAction), "Applied argument was null");
            if (newAction)
            {
                switch (financeAction.Type)
                {
                    case ActionType.DEPOSIT:
                        balance.Balance =  financeAction.Amount;
                        balance.BalanceDate = DateTime.Now;
                        break;
                    case ActionType.WITHDRAWAL:
                        balance.Balance =  - financeAction.Amount;
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
                        balance.Balance = balance.Balance + financeAction.Amount;
                        balance.BalanceDate = DateTime.Now;
                        break;
                    case ActionType.WITHDRAWAL:
                        balance.Balance = balance.Balance - financeAction.Amount;
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
