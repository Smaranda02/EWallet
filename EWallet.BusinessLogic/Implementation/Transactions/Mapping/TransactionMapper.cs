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
    public class TransactionMapper :Profile
    {
        public TransactionMapper() {
            CreateMap<CreateTransactionViewModel, Transaction>()
               .ForMember(a => a.TrasactionSum, a => a.MapFrom(s => s.TrasactionSum))
               .ForMember(a => a.UserId, a => a.MapFrom(s => s.UserId));

            CreateMap<Income, Transaction>()
              .ForMember(a => a.TrasactionSum, a => a.MapFrom(s => s.IncomeSum))
              .ForMember(a => a.Id, a => a.Ignore())
              .ForMember(a => a.TrasactionDateTime, a => a.MapFrom(s => DateTime.Now))
              .ForMember(a => a.UserId, a => a.MapFrom(s => s.UserId))
              .ForMember(a => a.IncomeId, a => a.MapFrom(s => s.Id))
              ;


            CreateMap<Spending, Transaction>()
            .ForMember(a => a.TrasactionSum, a => a.MapFrom(s => s.Amount))
            .ForMember(a => a.Id, a => a.Ignore())
            .ForMember(a => a.TrasactionDateTime, a => a.MapFrom(s => DateTime.Now))
            .ForMember(a => a.UserId, a => a.MapFrom(s => s.UserId))
            .ForMember(a => a.SpendingId, a => a.MapFrom(s => s.Id))
            ;

        }
    }
}
