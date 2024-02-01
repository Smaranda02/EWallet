using AutoMapper;
using EWallet.BusinessLogic.Implementation.PiggyBanks.ViewModel;
using EWallet.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.PiggyBanks.Mappings
{
    public class PiggyBanksIncomeViewModelMapper : Profile
    {
        public PiggyBanksIncomeViewModelMapper()
        {
            CreateMap<PiggyBanksIncome, PiggyBanksIncomeViewModel>()
              .ForMember(a => a.IncomeId, a => a.MapFrom(s => s.IncomeId))
              .ForMember(a => a.AllocatedIncomeAmount, a => a.MapFrom(s => s.AllocatedIncomeAmount))
              .ForMember(a => a.IncomeName, a => a.MapFrom(s => s.Income.IncomeDescription))
              .ForMember(a => a.PiggyBankId, a => a.MapFrom(s => s.PiggyBankId));

        }
    }
}
