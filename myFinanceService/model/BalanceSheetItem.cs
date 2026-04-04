namespace myFinanceService.Model
{
    public class BalanceSheetItem
    {
        public Guid Id { get; set; }
        public Guid BalanceSheetId { get; set; }
        public string ItemName { get; set; } = "";
        public double Value { get; set; }
        public myFinanceService.Domain.BalanceSheetItemType Type { get; set; }
    }
}
