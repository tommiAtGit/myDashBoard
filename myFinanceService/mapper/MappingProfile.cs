using AutoMapper;
using myFinanceService.Domain;
using myFinanceService.Model;

namespace myFinanceService.Mapper{
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateMap<Source, Destination>
        CreateMap<BalanceDTO, Balance>();
        CreateMap<Balance, BalanceDTO>();
        
        CreateMap<FinanceDTO, Finance>();
        CreateMap<Finance, FinanceDTO>();

        CreateMap<Budget, BudgetDTO>();
        CreateMap<BudgetDTO, Budget>();

    }
}
}
