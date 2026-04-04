using AutoMapper;
using myFinanceService.Domain;
using myFinanceService.Model;

namespace myFinanceService.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<Source, Destination>
            CreateMap<BalanceDTO, Balance>();
            CreateMap<Balance, BalanceDTO>();
            
            CreateMap<FinanceDTO, Finance>();
            CreateMap<Finance, FinanceDTO>();
            
            CreateMap<FinanceGategoryDTO, FinanceGategory>();
            CreateMap<FinanceGategory, FinanceGategoryDTO>();
            
            CreateMap<BalanceSheetDTO, BalanceSheet>();
            CreateMap<BalanceSheet, BalanceSheetDTO>();
            
            CreateMap<BalanceSheetItemDTO, BalanceSheetItem>();
            CreateMap<BalanceSheetItem, BalanceSheetItemDTO>();
            
            CreateMap<Budget, BudgetDTO>();
            CreateMap<BudgetDTO, Budget>();
        }
    }
}
