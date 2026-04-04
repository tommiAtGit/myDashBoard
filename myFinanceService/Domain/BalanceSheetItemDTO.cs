namespace myFinanceService.Domain
{
    public class BalanceSheetItemDTO
    {
        public Guid Id { get; set; }
        public Guid BalanceSheetId { get; set; }
        public string ItemName { get; set; } = "";
    }
}
