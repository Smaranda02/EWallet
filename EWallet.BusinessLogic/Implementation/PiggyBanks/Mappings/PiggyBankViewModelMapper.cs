using AutoMapper;
using AutoMapper.QueryableExtensions;
using EWallet.BusinessLogic.Implementation.PiggyBanks.ViewModel;
using EWallet.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.PiggyBanks.Mappings
{
    public class PiggyBankViewModelMapper : Profile
    {
        public PiggyBankViewModelMapper()
        {
            CreateMap<PiggyBank, PiggyBankViewModel>()
                .ForMember(a => a.TargetSum, a => a.MapFrom(s => s.TargetSum))
                 .ForMember(a => a.DueDate, a => a.MapFrom(s => s.DueDate))
                 .ForMember(a => a.PiggyBankDescription, a => a.MapFrom(s => s.PiggyBankDescription))
                 .ForMember(a => a.PiggyBankStatus, a => a.MapFrom(s => s.PiggyBankStatus))
                 .ForMember(a => a.PiggyBankStatusName, a => a.MapFrom(s => s.PiggyBankStatusNavigation.StatusName))
                 .ForMember(a => a.SavingPriority, a => a.MapFrom(s => s.SavingPriority))
                 
                 ;
        }
    }
}

