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
    public  class CurrentUserMapper : Profile
    {
      
        public CurrentUserMapper() {
            CreateMap<User, CurrentUserViewModel>()
               .ForMember(a => a.Id, a => a.MapFrom(s => s.Id))
               .ForMember(a => a.Email, a => a.MapFrom(s => s.Email))
               .ForMember(a => a.FirstName, a => a.MapFrom(s => s.FirstName))
               .ForMember(a => a.LastName, a => a.MapFrom(s => s.LastName))
               .ForMember(a => a.IsAuthenticated, a => a.MapFrom(s => true))
               .ForMember(a => a.Role, a => a.MapFrom(s => s.UserRoleId))
               .ForMember(a => a.Username, a => a.MapFrom(s => s.Username));
        }
    }
    
}
