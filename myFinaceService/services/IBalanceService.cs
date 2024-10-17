using myFinanceService.Domain;

namespace myFinanceService.Services
{
    public interface IBalanceService
    {
        public BalanceDTO AddNewBalance(FinanceDTO financeAction);
        public BalanceDTO UpdateBalance( String account, FinanceDTO financeAction);
        public BalanceDTO GetBalance(string account);
        public IEnumerable<BalanceDTO> GetAllBalances();

    }
}