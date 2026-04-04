using myFinanceService.Domain;

namespace myFinanceService.Repository
{
    public class MockBalanceSheetRepository : IMockBalanceSheetRepository
    {
        private List<BalanceSheetDTO> _balanceSheets;

        public MockBalanceSheetRepository()
        {
            _balanceSheets = new List<BalanceSheetDTO>();
            CreateTestData();
        }

        public BalanceSheetDTO AddBalanceSheet(BalanceSheetDTO balanceSheet)
        {
            balanceSheet.Id = Guid.NewGuid();
            balanceSheet.BalanceSheetItemCreated = DateTime.Now;
            balanceSheet.BalanceSheetItemChanged = DateTime.Now;
            _balanceSheets.Add(balanceSheet);
            return balanceSheet;
        }

        public bool DeleteBalanceSheet(Guid id)
        {
            var sheet = GetBalanceSheetById(id);
            if (sheet.Id == Guid.Empty) return false;
            return _balanceSheets.Remove(sheet);
        }

        public IEnumerable<BalanceSheetDTO> GetAllBalanceSheets()
        {
            return _balanceSheets;
        }

        public BalanceSheetDTO GetBalanceSheetById(Guid id)
        {
            return _balanceSheets.FirstOrDefault(s => s.Id == id) ?? new BalanceSheetDTO();
        }

        public BalanceSheetDTO UpdateBalanceSheet(Guid id, BalanceSheetDTO balanceSheet)
        {
            var existing = GetBalanceSheetById(id);
            if (existing.Id == Guid.Empty) return new BalanceSheetDTO();

            int index = _balanceSheets.IndexOf(existing);
            balanceSheet.Id = id;
            balanceSheet.BalanceSheetItemChanged = DateTime.Now;
            _balanceSheets[index] = balanceSheet;
            return balanceSheet;
        }

        private void CreateTestData()
        {
            _balanceSheets.Add(new BalanceSheetDTO
            {
                Id = Guid.Parse("a1a23202-3e9f-4284-8959-3cfa5cb39100"),
                BalanceSheetItemValue = 5000.0,
                BalanceSheetItemCreated = DateTime.Now.AddDays(-10),
                BalanceSheetItemChanged = DateTime.Now.AddDays(-5),
                Items = new List<BalanceSheetItemDTO>
                {
                    new BalanceSheetItemDTO { Id = Guid.NewGuid(), ItemName = "Asset 1" },
                    new BalanceSheetItemDTO { Id = Guid.NewGuid(), ItemName = "Asset 2" }
                }
            });
        }
    }
}
