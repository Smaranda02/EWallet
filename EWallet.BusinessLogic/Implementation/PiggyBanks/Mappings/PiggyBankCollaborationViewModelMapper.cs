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
    public class PiggyBankCollaborationViewModelMapper :Profile
    {
        public PiggyBankCollaborationViewModelMapper()
        {
            CreateMap<PiggyBanksFriend, PiggyBankCollaborationViewModel>()
               .ForMember(a => a.UserId, a => a.MapFrom(s => s.UserId))
               .ForMember(a => a.PiggyBankId, a => a.MapFrom(s => s.PiggyBankId))
               .ForMember(a => a.Id, a => a.MapFrom(s => s.Id))
               ;

        }
    }
}
