using AutoMapper;
using EWallet.BusinessLogic.Implementation.Incomes.ViewModel;
using EWallet.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Incomes.Mappings
{
    public  class IncomeMapper :Profile
    {
        public IncomeMapper() {

            CreateMap<CreateIncomeViewModel, Income>()

                    .ForMember(a => a.IncomeSum, a => a.MapFrom(s => s.IncomeSum))
                    .ForMember(a => a.IncomeDescription, a => a.MapFrom(s => s.IncomeDescription))
                    .ForMember(a => a.RecurrenceTypeId, a => a.MapFrom(s => s.RecurrenceTypeId))
                    .ForMember(a => a.RecurringNumber, a => a.MapFrom(s => s.RecurringNumber))
                    .ForMember(a => a.Id, a => a.Ignore());

            CreateMap<EditIncomeViewModel, Income>()

                  .ForMember(a => a.IncomeSum, a => a.MapFrom(s => s.IncomeSum))
                  .ForMember(a => a.IncomeDescription, a => a.MapFrom(s => s.IncomeDescription))
                  .ForMember(a => a.RecurringNumber, a => a.MapFrom(s => s.RecurringNumber))
                  .ForMember(a => a.RecurrenceTypeId, a => a.MapFrom(s => s.RecurrenceTypeId))
                  .ForMember(a => a.IsDeleted, a => a.MapFrom(s => false))
                  .ForMember(a => a.Id, a => a.MapFrom(s => s.Id));



        }
    }
}
