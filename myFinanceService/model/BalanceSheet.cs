namespace myFinanceService.Model
{
    public class BalanceSheet
    {
        public Guid Id { get; set; }
        public double BalanceSheetItemValue { get; set; }
        public DateTime BalanceSheetItemCreated { get; set; }
        public DateTime BalanceSheetItemChanged { get; set; }
        public List<BalanceSheetItem> Items { get; set; } = new();
    }
}
