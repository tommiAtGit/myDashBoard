
namespace myFinanceService.Domain{
    public class BudgetDTO{
         public Guid Id {get;set;}
        public string BudgetAccount {get;set;} = "";
        public string BudgetTitle {get;set;} = "";
        public double BudgetValue {get;set;}
        public DateTime BudgetStartDate {get;set;}
        public DateTime BudgetEndDate {get;set;}
    }
}