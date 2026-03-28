using Microsoft.AspNetCore.Http.HttpResults;
using myFinanceService.Domain;
using System.Linq;

namespace myFinanceService.Repository
{
    public class MockBalanceRepository : IMockBalanceRepository
    {
        private List<BalanceDTO> _balance;

        public MockBalanceRepository()
        {
          
                _balance = [];
            
        }

        public BalanceDTO AddNewBalance(BalanceDTO balanceAction)
        {
           balanceAction.Id = Guid.NewGuid();
            _balance.Add(balanceAction);
            return balanceAction;

        }


        public IEnumerable<BalanceDTO> GetAllBalances(){
            return _balance;
         }

        public BalanceDTO GetBalance(string account)
        {
            if ((account == null) || (account ==""))
                throw new ArgumentException("Argument was null or empty", nameof(account));
            
            var balance = _balance.FirstOrDefault(a => a.Account == account);
            if (balance == null )
                throw new ArgumentNullException();
            return balance;
        }

        public IEnumerable<BalanceDTO> GetBalanceHistory(string account)
        {
            List<BalanceDTO> balanceDTOs = [];
            foreach (BalanceDTO balance in _balance)
            {
                if (balance.Account.Equals(account))
                {
                    balanceDTOs.Add(balance);
                }
            }
            return balanceDTOs;


        }
        public BalanceDTO UpdateBalance(string account, BalanceDTO balanceAction)
        {
           var balance = GetBalance(account);
           balance.AccountBalance = balanceAction.AccountBalance;
           balance.BalanceDate = balanceAction.BalanceDate;
          

           return balance;

        }

        public bool DeleteBalance(Guid id)
        {
            var balances = GetAllBalances();
            var balance = balances.FirstOrDefault(a=>a.Id == id);
            if(balance == null)
                return false;
            return _balance.Remove(balance);

        }

       
    }
}