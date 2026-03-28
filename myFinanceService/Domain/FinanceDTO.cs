using myFinanceService.Domain;
namespace myFinanceService.Domain
{

    public class FinanceDTO
    {

        public Guid Id { get; set; }
        public ActionType Type { get; set; }
        public string Account { get; set; } = "";
        public string Description { get; set; }="";
        public Double Amount { get; set; }
        public DateTime ActionDate { get; set; }
        

    }
}