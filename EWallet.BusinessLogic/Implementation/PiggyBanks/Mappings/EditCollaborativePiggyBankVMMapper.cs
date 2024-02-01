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
    public class EditCollaborativePiggyBankVMMapper :Profile
    {
        public EditCollaborativePiggyBankVMMapper()
        {
            CreateMap<PiggyBank, EditCollaborativePiggyBankViewModel>()
                .ForMember(a => a.Id, a => a.MapFrom(s => s.Id))
                ;
        }
    }
}
