namespace myFinanceService.Model
{
    public class BalanceSheetItem
    {
        public Guid Id { get; set; }
        public Guid BalanceSheetId { get; set; }
        public string ItemName { get; set; } = "";
    }
}
