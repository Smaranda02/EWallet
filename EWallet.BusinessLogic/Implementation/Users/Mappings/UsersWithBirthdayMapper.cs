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
    public class UsersWithBirthdayMapper : Profile
    {
        public UsersWithBirthdayMapper()
        {
            CreateMap<VwUpcomingBirthday, UserWithBirthdayViewModel>()
               .ForMember(a => a.Username, a => a.MapFrom(s => s.Username))
               .ForMember(a => a.Id, a => a.MapFrom(s => s.Id))
               ;
        }
    }
}
