using AutoMapper;
using EWallet.BusinessLogic.Implementation.Users.ViewModel;
using EWallet.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Users.Mappings
{
    public class FriendViewModelMapper :Profile
    {
        public FriendViewModelMapper()
        {
            CreateMap<User, FriendViewModel>()
                .ForMember(a => a.Username, a => a.MapFrom(s => s.Username))
                .ForMember(a => a.UserId, a => a.MapFrom(s => s.Id))
                ;
        }
    }
}
