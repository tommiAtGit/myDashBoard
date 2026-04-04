using myFinanceService.Model;

namespace myFinanceService.Services
{
    public interface IBalanceSheetService
    {
        BalanceSheet AddBalanceSheet(BalanceSheet balanceSheet);
        bool DeleteBalanceSheet(Guid id);
        IEnumerable<BalanceSheet> GetAllBalanceSheets();
        BalanceSheet GetBalanceSheetById(Guid id);
        BalanceSheet UpdateBalanceSheet(Guid id, BalanceSheet balanceSheet);
    }
}
