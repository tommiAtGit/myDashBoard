using Microsoft.AspNetCore.SignalR;

namespace myFinanceService.Domain
{
    public class BalanceDTO
    {
        public Guid Id;
        public string Account { get; set; } = "";
        public Double Balance { get; set; }
        public DateTime BalanceDate { get; set; }
    }
}