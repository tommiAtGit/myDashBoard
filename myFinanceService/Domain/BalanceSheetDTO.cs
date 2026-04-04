namespace myFinanceService.Domain
{
    public class BalanceSheetDTO
    {
        public Guid Id { get; set; }
        public double BalanceSheetItemValue { get; set; }
        public DateTime BalanceSheetItemCreated { get; set; }
        public DateTime BalanceSheetItemChanged { get; set; }
        public List<BalanceSheetItemDTO> Items { get; set; } = new();
    }
}
