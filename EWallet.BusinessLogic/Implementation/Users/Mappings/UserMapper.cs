using AutoMapper;
using EWallet.DataAccess.Enums;
using EWallet.Entities.Entities;
using System;

namespace EWallet.BusinessLogic.Implementation.Users
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<RegistrationViewModel, User>()
                .ForMember(a => a.Email, a => a.MapFrom(s => s.Email))
                .ForMember(a => a.FirstName, a => a.MapFrom(s => s.FirstName))
                .ForMember(a => a.LastName, a => a.MapFrom(s => s.LastName))
                .ForMember(a => a.Birthdate, a => a.MapFrom(s => s.BirthDate))
                .ForMember(a => a.UserPassword, a => a.Ignore())
                .ForMember(a => a.UserRoleId, a => a.MapFrom(s => Roles.User))
                ;


        }
    }
}
