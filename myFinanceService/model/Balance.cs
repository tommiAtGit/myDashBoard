namespace myFinanceService.Model{
    public class Balance{
         public Guid Id;
        public string Account { get; set; } = "";
        public Double AccountBalance { get; set; }
        public DateTime BalanceDate { get; set; }
    }
}