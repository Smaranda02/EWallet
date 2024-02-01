using EWallet.BusinessLogic.Implementation.Incomes.ViewModel;
using EWallet.Entities.Entities;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Incomes.Mappings
{
    public  class IncomeViewModelMapper :Profile
    {
        public IncomeViewModelMapper()
        {
            CreateMap<Income, IncomeViewModel>()

                    .ForMember(a => a.IncomeSum, a => a.MapFrom(s => s.IncomeSum))
                    .ForMember(a => a.IncomeDescription, a => a.MapFrom(s => s.IncomeDescription))
                    .ForMember(a => a.RecurrenceTypeName, a => a.MapFrom(s => s.RecurrenceType.RecurrenceTypeName))
                    //.ForMember(a => a.PiggyBankIds, a => a.Ignore())
                    .ForMember(a => a.RecurrenceTypeId, a => a.MapFrom(s => s.RecurrenceTypeId));



        }

    }
}
