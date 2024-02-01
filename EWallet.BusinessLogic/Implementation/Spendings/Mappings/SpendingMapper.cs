using AutoMapper;
using EWallet.BusinessLogic.Implementation.Spendings.ViewModel;
using EWallet.BusinessLogic.Implementation.Users.ViewModel;
using EWallet.Entities.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Spendings.Mappings
{
    public class SpendingMapper : Profile
    {

        public SpendingMapper() 
        {

            CreateMap<CreateSpendingViewModel, Spending>()
               .ForMember(a => a.Amount, a => a.MapFrom(s => s.Amount))
               .ForMember(a => a.SpendingCategoryId, a => a.MapFrom(s => s.SpendingCategoryId))
               .ForMember(a => a.SpendingDescription, a => a.MapFrom(s => s.SpendingDescription))
               .ForMember(a => a.RecurringNumber, a => a.MapFrom(s => s.RecurringNumber))
               .ForMember(a => a.RecurrenceTypeId, a => a.MapFrom(s => s.RecurrenceTypeId))
               .ForMember(a => a.Id, a => a.Ignore());



            CreateMap<EditSpendingViewModel, Spending>()
               .ForMember(a => a.Amount, a => a.MapFrom(s => s.Amount))
               .ForMember(a => a.SpendingDescription, a => a.MapFrom(s => s.SpendingDescription))
               .ForMember(a => a.SpendingCategoryId, a => a.MapFrom(s => s.SpendingCategoryId))
               .ForMember(a => a.RecurrenceTypeId, a => a.MapFrom(s => s.RecurrenceTypeId))
               .ForMember(a => a.RecurringNumber, a => a.MapFrom(s => s.RecurringNumber))
               .ForMember(a => a.IsDeleted, a => a.MapFrom(s => false))
               .ForMember(a => a.Id, a => a.MapFrom(s => s.Id));


            CreateMap<SpendingViewModel, Spending>()
                .ForMember(a => a.Amount, a => a.MapFrom(s => s.Amount))
                .ForMember(a => a.SpendingCategoryId, a => a.MapFrom(s => s.SpendingCategoryId))
                 .ForMember(a => a.SpendingDescription, a => a.MapFrom(s => s.SpendingDescription))
                 .ForMember(a => a.Id, a => a.MapFrom(s => s.Id))
                 ;



            CreateMap<PiggyBank, Spending>()
                .ForMember(a => a.Amount, a => a.MapFrom(s => s.TargetSum))
                 .ForMember(a => a.SpendingDescription, a => a.MapFrom(s => s.PiggyBankDescription))
                  .ForMember(a => a.IsDeleted, a => a.MapFrom(s => false))
                 .ForMember(a => a.Id, a => a.Ignore())
                 ;





        }




    }
}
