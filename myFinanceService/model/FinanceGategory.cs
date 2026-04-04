namespace myFinanceService.Model
{
    public class FinanceGategory
    {
        public Guid Id { get; set; }
        public Guid FinanceId { get; set; }
        public string Category { get; set; } = "";
    }
}
