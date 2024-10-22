using myFinanceService.Domain;

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
        public List<FinanceDTO> CreateNewMocTransactionsWithDifferentAccount()
        {

            List<FinanceDTO> transactions = new();
            for (int i = 0; i < NUMBER_OF_TRANSACTIONS; i++)
            {

                FinanceDTO dto = new FinanceDTO();
                dto.Id = Guid.NewGuid();
                dto.Type = ActionType.WITHDRAWAL;
                dto.Account = FIRST_ACCOUNT;
                dto.Description = "Save to my Account_action#" + i + ".0";
                dto.Amount = 3 * i;
                dto.ActionDate = DateTime.Now;
                transactions.Add(dto);

                FinanceDTO dto_a = new FinanceDTO();
                dto.Id = Guid.NewGuid();
                dto_a.Type = ActionType.DEPOSIT;
                dto_a.Account = FIRST_ACCOUNT;
                dto_a.Description = "Save to my Account_action#" + i + ".1";
                dto_a.Amount = 10 * i;
                dto_a.ActionDate = DateTime.Now;
                transactions.Add(dto_a);

                FinanceDTO dto_b = new FinanceDTO();
                dto.Id = Guid.NewGuid();
                dto_b.Type = ActionType.WITHDRAWAL;
                dto_b.Account = SECOND_ACCOUNT;
                dto_b.Description = "Save to my Account_action#" + i + ".2";
                dto_b.Amount = 3 * i;
                dto_b.ActionDate = DateTime.Now;
                transactions.Add(dto_b);

                FinanceDTO dto_c = new FinanceDTO();
                dto.Id = Guid.NewGuid();
                dto_c.Type = ActionType.DEPOSIT;
                dto_c.Account = SECOND_ACCOUNT;
                dto_c.Description = "Save to my Account_action#" + i + ".3";
                dto_c.Amount = 10 * i;
                dto_c.ActionDate = DateTime.Now;
                transactions.Add(dto_c);

            }

            return transactions;

        }
        public FinanceDTO CreateNewMocDepositTransaction(Guid id)
        {
            FinanceDTO dto = new FinanceDTO();
            dto.Id = id;
            dto.Type = ActionType.DEPOSIT;
            dto.Account = FIRST_ACCOUNT;
            dto.Description = "Save single deposit to my Account";
            dto.Amount = 200;
            dto.ActionDate = DateTime.Now;
            return dto;
        }
        public FinanceDTO CreateNewMocDepositTransaction()
        {
            FinanceDTO dto = new FinanceDTO
            {
                Type = ActionType.DEPOSIT,
                Account = FIRST_ACCOUNT,
                Description = "Save single deposit to my Account",
                Amount = 200,
                ActionDate = DateTime.Now
            };
            return dto;
        }

        public List<BalanceDTO> CreateNewMocBalancesWithDifferentAccount()
        {

            List<BalanceDTO> balanceDTOs = [];
            for (int i = 0; i < NUMBER_OF_BALANCES; i++)
            {
                BalanceDTO balance = new()
                {
                    Id = Guid.NewGuid()
                };
                if (i % 2 == 0)
                    balance.Account = FIRST_ACCOUNT;
                else
                    balance.Account = SECOND_ACCOUNT;
                balance.Balance = 200;
                balance.BalanceDate = DateTime.Now.AddDays(-i);
                balanceDTOs.Add(balance);


            }
            return balanceDTOs;

        }

        public BalanceDTO CreateBalanceWithFirstAccount()
        {
            BalanceDTO balance = new()
            {
                Id = Guid.NewGuid(),
                Account = FIRST_ACCOUNT,
                Balance = 200,
                BalanceDate = DateTime.Now.AddDays(-2)
            };
            return balance;
        }

    }
}