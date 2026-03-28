using myFinanceService.Domain;

namespace myFinanceService.Repository
{
    public interface IMockBalanceRepository
    {
        public BalanceDTO AddNewBalance(BalanceDTO balanceAction);
        public BalanceDTO UpdateBalance(String account, BalanceDTO balanceAction);

        public BalanceDTO GetBalance(string account);
        public IEnumerable<BalanceDTO> GetBalanceHistory(string account);
         public IEnumerable<BalanceDTO> GetAllBalances();

         public bool DeleteBalance(Guid id);


    }


}