using myFinanceService.Domain;
using myFinanceService.Model;

namespace myFinanceService.Services
{
    public interface IBalanceService
    {
        public Balance AddNewBalance(Finance financeAction);
        public Balance UpdateBalance( String account, Finance financeAction);
        public Balance GetBalance(string account);
        public IEnumerable<Balance> GetAllBalances();

        public bool DeleteBalance(Guid id);

    }
}