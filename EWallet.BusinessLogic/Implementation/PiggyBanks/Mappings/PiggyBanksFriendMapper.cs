using AutoMapper;
using EWallet.BusinessLogic.Implementation.PiggyBanks.ViewModel;
using EWallet.BusinessLogic.Implementation.Users.ViewModel;
using EWallet.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.PiggyBanks.Mappings
{
    public class PiggyBanksFriendMapper : Profile
    {
        public PiggyBanksFriendMapper()
        {
            CreateMap<PiggyBankCollaborationViewModel, PiggyBanksFriend>()
              .ForMember(a => a.UserId, a => a.MapFrom(s => s.UserId))
              .ForMember(a => a.PiggyBankId, a => a.MapFrom(s => s.PiggyBankId))
              ;

        }

    }
}
