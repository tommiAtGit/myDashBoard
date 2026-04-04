using AutoMapper;
using myFinanceService.Domain;
using myFinanceService.Model;
using myFinanceService.Repository;

namespace myFinanceService.Services
{
    public class BalanceSheetService : IBalanceSheetService
    {
        private readonly IMockBalanceSheetRepository _repository;
        private readonly IMapper _mapper;

        public BalanceSheetService(IMockBalanceSheetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public BalanceSheet AddBalanceSheet(BalanceSheet balanceSheet)
        {
            var dto = _mapper.Map<BalanceSheetDTO>(balanceSheet);
            var resultDto = _repository.AddBalanceSheet(dto);
            return _mapper.Map<BalanceSheet>(resultDto);
        }

        public bool DeleteBalanceSheet(Guid id)
        {
            return _repository.DeleteBalanceSheet(id);
        }

        public IEnumerable<BalanceSheet> GetAllBalanceSheets()
        {
            var dtos = _repository.GetAllBalanceSheets();
            return _mapper.Map<IEnumerable<BalanceSheet>>(dtos);
        }

        public BalanceSheet GetBalanceSheetById(Guid id)
        {
            var dto = _repository.GetBalanceSheetById(id);
            if (dto.Id == Guid.Empty) return new BalanceSheet();
            return _mapper.Map<BalanceSheet>(dto);
        }

        public BalanceSheet UpdateBalanceSheet(Guid id, BalanceSheet balanceSheet)
        {
            var dto = _mapper.Map<BalanceSheetDTO>(balanceSheet);
            var resultDto = _repository.UpdateBalanceSheet(id, dto);
            if (resultDto.Id == Guid.Empty) return new BalanceSheet();
            return _mapper.Map<BalanceSheet>(resultDto);
        }
    }
}
