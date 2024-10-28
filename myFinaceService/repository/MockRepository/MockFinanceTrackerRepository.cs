using myFinanceService.Domain;
using System.Collections.Generic;
using System.Linq;

namespace myFinanceService.Repository
{
    public class MockFinanceTrackerRepository : IMockFinanceTrackerRepository
    {
        private List<FinanceDTO> _financeRepo;
        public MockFinanceTrackerRepository()
        {
            _financeRepo = [];
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

               
                if ((dto.ActionDate.CompareTo(startTime)<1) && (dto.ActionDate.CompareTo(EndTime)==1))
                {
                    _financeByDates.Add(dto);
                }
            }
            if (_financeRepo != null)
                return _financeByDates;
            else
            {
                throw new ArgumentNullException("No entries found with selected period ", nameof(_financeByDates));
            }
        }

        public FinanceDTO GetTransactionById(Guid id)
        {

            try
            {
                var dto = _financeRepo.FirstOrDefault(p => p.Id == id);

                ArgumentNullException.ThrowIfNull(dto);
                return dto;

            }
            catch (ArgumentNullException)
            {
                throw new Exception();
            }



        }

        public IEnumerable<FinanceDTO> GetTransactionsByAccount(string AccountId)
        {
           var accounts = _financeRepo.FindAll(a=> a.Account == AccountId );
           ArgumentNullException.ThrowIfNull(accounts);

           return accounts;
        }

        public FinanceDTO UpdateTransaction(Guid Id, FinanceDTO financeDTO)
        {
            
            FinanceDTO dto = GetTransactionById(Id);
            int index = _financeRepo.IndexOf(dto);

            dto.Type = financeDTO.Type;
            dto.Description = financeDTO.Description;
            dto.Amount = financeDTO.Amount;
            dto.ActionDate = financeDTO.ActionDate;
            _financeRepo.Insert(index, dto);

            return dto;
        }
    }

}