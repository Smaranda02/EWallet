using AutoMapper;
using EWallet.BusinessLogic.Implementation.Transactions.ViewModel;
using EWallet.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Transactions.Mapping
{
    public class TansactionViewModelMapper : Profile
    {
        public TansactionViewModelMapper() {
            CreateMap<Transaction, TransactionViewModel>()
                .ForMember(a => a.Id, a => a.MapFrom(s => s.Id))
                .ForMember(a => a.TrasactionSum, a => a.MapFrom(s => s.TrasactionSum))
                .ForMember(a => a.TrasactionDate, a => a.MapFrom(s => s.TrasactionDateTime.Date.ToString("dd/MM/yyyy")))
                .ForMember(a => a.TransactionTime, a => a.MapFrom(s => s.TrasactionDateTime.TimeOfDay.ToString(@"hh\:mm\:ss")))
                .ForMember(a => a.Username, a => a.MapFrom(s => s.User.Username))
                .ForMember(a => a.IncomeId, a => a.MapFrom(s => s.IncomeId))
                .ForMember(a => a.SpendingId, a => a.MapFrom(s => s.SpendingId))
                .ForMember(a => a.PiggyBankId, a => a.MapFrom(s => s.PiggyBankId))
                .ForMember(a => a.SpendingCategory, a => a.MapFrom(s => s.Spending.SpendingCategory.CategoryName))

                ;

        }
    }
}
