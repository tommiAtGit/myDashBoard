using AutoMapper;
using myFinanceService.Domain;
using myFinanceService.Model;
using myFinanceService.Repository;

namespace myFinanceService.Services
{
    public class BudgetService : IBudgetService
    {
        private IMockBudgetRepository _repository;
        private IMapper _mapper;

        public BudgetService(IMapper mapper){

            _mapper = mapper;
            _repository = new MockBudgetRepository();

        }
        public Budget AddBudget(Budget newBudget)
        {
           BudgetDTO dto = _repository.AddBudget(_mapper.Map<BudgetDTO>(newBudget));
           return _mapper.Map<Budget>(dto);

        }

        public IEnumerable<Budget> GetAllBudgets()
        {
          return _mapper.Map<IEnumerable<Budget>>(_repository.GetAllBudgets());
        }

        public bool DeleteBudget(Guid id)
        {
            if (id == Guid.Empty )
                throw new ArgumentException(nameof(id), "Applied id was null or empty");

           return _repository.DeleteBudget(id);
        }

        public Budget GetBudgetByAccount(string account)
        {
           if((account == null)||(account==""))
            throw new ArgumentException(nameof(account), "Applied account was null or empty");

            return _mapper.Map<Budget>(_repository.GetBudgetByAccount(account));
        }

        public Budget GetBudgetById(Guid id)
        {
             if (id == Guid.Empty )
                throw new ArgumentException(nameof(id), "Applied id was null or empty");

            return _mapper.Map<Budget>(_repository.GetBudgetById(id));
        }

        public Budget UpdateAccountBudget(Guid id, Budget budget)
        {
            if (id == Guid.Empty )
                throw new ArgumentException(nameof(id), "Applied id was null or empty");


             return _mapper.Map<Budget>(_repository.UpdateBudget(id, _mapper.Map<BudgetDTO>(budget)));
        }
    }
}