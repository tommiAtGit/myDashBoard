namespace myFinanceService.Domain
{
    public class BalanceSheetItemDTO
    {
        public Guid Id { get; set; }
        public Guid BalanceSheetId { get; set; }
        public string ItemName { get; set; } = "";
        public double Value { get; set; }
        public BalanceSheetItemType Type { get; set; }
    }
}
