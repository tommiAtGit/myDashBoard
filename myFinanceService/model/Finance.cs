using  myFinanceService.Domain;

namespace myFinanceService.Model{
    public class Finance{
         public Guid Id { get; set; }
        public ActionType Type { get; set; }
        public string Account { get; set; } = "";
        public string Description { get; set; }="";
        public Double Amount { get; set; }
        public DateTime ActionDate { get; set; }
    }
}