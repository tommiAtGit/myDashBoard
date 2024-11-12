using myFinanceService.Domain;
using myFinanceService.Model;

namespace myFinanceService.TestUtils
{
    public class FinanceServiceTestUtils
    {
        private const string FIRST_ACCOUNT = "FI2180000012345678";
        private const string SECOND_ACCOUNT = "FI3080010012345690";
        private const int NUMBER_OF_TRANSACTIONS = 5;
        private const int NUMBER_OF_BALANCES = 6;

        public string GetFirstAccount()
        {
            return FIRST_ACCOUNT;
        }
        public string GetSecondAccount()
        {
            return SECOND_ACCOUNT;
        }
        public int GetNumberOfBalances()
        {
            return NUMBER_OF_BALANCES;
        }
        public int GetNumberOfTransactions()
        {
            return NUMBER_OF_TRANSACTIONS;
        }
        public List<Finance> CreateNewMocTransactionsWithDifferentAccount()
        {

            List<Finance> transactions = new();
            for (int i = 0; i < NUMBER_OF_TRANSACTIONS; i++)
            {

                Finance dto = new()
                {
                    Id = Guid.NewGuid(),
                    Type = ActionType.WITHDRAWAL,
                    Account = FIRST_ACCOUNT,
                    Description = "Save to my Account_action#" + i + ".0",
                    Amount = 3 * i,
                    ActionDate = DateTime.Now
                };
                transactions.Add(dto);

                Finance dto_a = new()
                {
                    Id = Guid.NewGuid(),
                    Type = ActionType.DEPOSIT,
                    Account = FIRST_ACCOUNT,
                    Description = "Save to my Account_action#" + i + ".1",
                    Amount = 10 * i,
                    ActionDate = DateTime.Now
                };
                transactions.Add(dto_a);


                Finance dto_b = new()
                {
                    Id = Guid.NewGuid(),
                    Type = ActionType.WITHDRAWAL,
                    Account = SECOND_ACCOUNT,
                    Description = "Save to my Account_action#" + i + ".2",
                    Amount = 3 * i,
                    ActionDate = DateTime.Now,
                };
                transactions.Add(dto_b);

                Finance dto_c = new()
                {
                    Id = Guid.NewGuid(),
                    Type = ActionType.DEPOSIT,
                    Account = SECOND_ACCOUNT,
                    Description = "Save to my Account_action#" + i + ".3",
                    Amount = 10 * i,
                    ActionDate = DateTime.Now
                };

                transactions.Add(dto_c);

            }

            return transactions;

        }
        public Finance CreateNewMocDepositTransaction(Guid id)
        {
            Finance dto = new()
            {
                Id = id,
                Type = ActionType.DEPOSIT,
                Account = FIRST_ACCOUNT,
                Description = "Save single deposit to my Account",
                Amount = 200,
                ActionDate = DateTime.Now
            };
            return dto;
        }
        public Finance CreateNewMocDepositTransaction()
        {
            Finance dto = new Finance
            {
                Type = ActionType.DEPOSIT,
                Account = FIRST_ACCOUNT,
                Description = "Save single deposit to my Account",
                Amount = 200,
                ActionDate = DateTime.Now
            };
            return dto;
        }

        public List<Balance> CreateNewMocBalancesWithDifferentAccount()
        {

            List<Balance> balances = [];
            for (int i = 0; i < NUMBER_OF_BALANCES; i++)
            {
                Balance balance = new()
                {
                    Id = Guid.NewGuid()
                };
                if (i % 2 == 0)
                    balance.Account = FIRST_ACCOUNT;
                else
                    balance.Account = SECOND_ACCOUNT;
                balance.AccountBalance = 200;
                balance.BalanceDate = DateTime.Now.AddDays(-i);
                balances.Add(balance);


            }
            return balances;

        }

        public Balance CreateBalanceWithFirstAccount()
        {
            Balance balance = new()
            {
                Id = Guid.NewGuid(),
                Account = FIRST_ACCOUNT,
                AccountBalance = 200,
                BalanceDate = DateTime.Now.AddDays(-2)
            };
            return balance;
        }

        public Budget CreateBudget()
        {
            Budget budget = new()
            {

                Id = Guid.NewGuid(),
                BudgetAccount = FIRST_ACCOUNT,
                BudgetTitle = "Test budget",
                BudgetValue = 10000,
                BudgetStartDate = DateTime.Now.AddDays(-2),
                BudgetEndDate = DateTime.Now.AddDays(-1)
            };

            return budget;
        }

        public List<Budget> CreateBudgetWithDifferentAccount()
        {
            List<Budget> budgets = [];

            Budget b1 = new()
            {

                Id = Guid.NewGuid(),
                BudgetAccount = FIRST_ACCOUNT,
                BudgetTitle = "Test budget",
                BudgetValue = 10000,
                BudgetStartDate = DateTime.Now.AddDays(-2),
                BudgetEndDate = DateTime.Now.AddDays(-1)
            };
            budgets.Add(b1);

            Budget b2 = new()
            {

                Id = Guid.NewGuid(),
                BudgetAccount = FIRST_ACCOUNT,
                BudgetTitle = "Test budget",
                BudgetValue = 10000,
                BudgetStartDate = DateTime.Now.AddDays(-2),
                BudgetEndDate = DateTime.Now.AddDays(-1)
            };
            budgets.Add(b2);

            return budgets;
        }

    }
}