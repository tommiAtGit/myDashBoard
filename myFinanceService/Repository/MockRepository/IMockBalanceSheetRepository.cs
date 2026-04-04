using myFinanceService.Domain;

namespace myFinanceService.Repository
{
    public interface IMockBalanceSheetRepository
    {
        BalanceSheetDTO AddBalanceSheet(BalanceSheetDTO balanceSheet);
        bool DeleteBalanceSheet(Guid id);
        IEnumerable<BalanceSheetDTO> GetAllBalanceSheets();
        BalanceSheetDTO GetBalanceSheetById(Guid id);
        BalanceSheetDTO UpdateBalanceSheet(Guid id, BalanceSheetDTO balanceSheet);
    }
}
