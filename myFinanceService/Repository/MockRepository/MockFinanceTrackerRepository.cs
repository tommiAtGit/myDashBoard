using myFinanceService.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace myFinanceService.Repository
{
    public class MockFinanceTrackerRepository : IMockFinanceTrackerRepository
    {
        private List<FinanceDTO> _financeRepo;

        private const string FIRST_ACCOUNT = "FI2180000012345678";
        private const string SECOND_ACCOUNT = "FI3080010012345690";
        private const int NUMBER_OF_TRANSACTIONS = 5;
        public MockFinanceTrackerRepository()
        {
            _financeRepo = [];
            CreateTestData();
        }
        public FinanceDTO AddNewTransaction(FinanceDTO financeDTO)
        {
            financeDTO.Id = Guid.NewGuid();
            _financeRepo.Add(financeDTO);

            return financeDTO;
        }

        public bool DeleteTransaction(Guid id)
        {
            FinanceDTO dto = GetTransactionById(id);
            bool result = _financeRepo.Remove(dto);
            return result;


        }

        public IEnumerable<FinanceDTO> GetAllTransactions()
        {

            return _financeRepo;
        }

        public IEnumerable<FinanceDTO> GetTransactionByDate(DateTime startTime, DateTime EndTime)
        {
            List<FinanceDTO> _financeByDates;
            _financeByDates = [];

            foreach (FinanceDTO dto in _financeRepo)
            {


                if ((dto.ActionDate.Date.CompareTo(startTime.Date) > 0) && (dto.ActionDate.Date.CompareTo(EndTime.Date) < 0))
                {
                    _financeByDates.Add(dto);
                }
            }
            if (_financeByDates.Count() > 0)
                return _financeByDates;
            else
            {
                return [];
            }
        }

        public FinanceDTO GetTransactionById(Guid id)
        {

            try
            {
                var dto = _financeRepo.FirstOrDefault(p => p.Id == id);
                if (dto== default)
                    return new FinanceDTO();
                return dto;

            }
            catch (ArgumentNullException)
            {
                throw new Exception();
            }



        }

        public IEnumerable<FinanceDTO> GetTransactionsByAccount(string AccountId)
        {
            var accounts = _financeRepo.FindAll(a => a.Account == AccountId);
            if (accounts==default)
                return [];
            return accounts;
        }

        public FinanceDTO UpdateTransaction(Guid Id, FinanceDTO financeDTO)
        {


            FinanceDTO dto = GetTransactionById(Id);
            if (dto.Id == Guid.Empty)
                return new FinanceDTO();
            else
            {
                int index = _financeRepo.IndexOf(dto);

                dto.Type = financeDTO.Type;
                dto.Description = financeDTO.Description;
                dto.Amount = financeDTO.Amount;
                dto.ActionDate = financeDTO.ActionDate;
                _financeRepo.Insert(index, dto);

                return dto;

            }

        }

        private void CreateTestData()
        {
            var cultureInfo = new CultureInfo("fi-FI");
            for (int i = 0; i < NUMBER_OF_TRANSACTIONS; i++)
            {

                FinanceDTO dto = new()
                {
                    Id = new Guid("b1a23202-3e9f-4284-8959-3cfa5cb391" + i + "0"),
                    Type = ActionType.WITHDRAWAL,
                    Account = FIRST_ACCOUNT,
                    Description = "Save to my Account_action#" + i + ".0",
                    Amount = 3 * i,
                    ActionDate = DateTime.Parse("2024-11-06T13:34:35.900242+02:00", cultureInfo)
                    
                };
                _financeRepo.Add(dto);

                FinanceDTO dto_a = new()
                {
                    Id = new Guid("b1a23202-3e9f-4284-8959-3cfa5cb391" + i + "1"),
                    Type = ActionType.DEPOSIT,
                    Account = FIRST_ACCOUNT,
                    Description = "Save to my Account_action#" + i + ".1",
                    Amount = 10 * i,
                    ActionDate = DateTime.Parse("2024-11-03T13:34:35.900242+02:00", cultureInfo)
                };
                _financeRepo.Add(dto_a);


                FinanceDTO dto_b = new()
                {
                    Id = new Guid("b1a23202-3e9f-4284-8959-3cfa5cb391" + i + "2"),
                    Type = ActionType.WITHDRAWAL,
                    Account = SECOND_ACCOUNT,
                    Description = "Save to my Account_action#" + i + ".2",
                    Amount = 3 * i,
                    ActionDate = DateTime.Parse("2024-11-02T13:34:35.900242+02:00", cultureInfo)
                };
                _financeRepo.Add(dto_b);

                FinanceDTO dto_c = new()
                {
                    Id = new Guid("b1a23202-3e9f-4284-8959-3cfa5cb391" + i + "3"),
                    Type = ActionType.DEPOSIT,
                    Account = SECOND_ACCOUNT,
                    Description = "Save to my Account_action#" + i + ".3",
                    Amount = 10 * i,
                    ActionDate = DateTime.Now
                };
                _financeRepo.Add(dto_c);
            }

        }


    }

}