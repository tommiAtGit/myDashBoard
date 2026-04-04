namespace myFinanceService.Domain
{
    public class FinanceGategoryDTO
    {
        public Guid Id { get; set; }
        public Guid FinanceId { get; set; }
        public string Category { get; set; } = "";
    }
}
