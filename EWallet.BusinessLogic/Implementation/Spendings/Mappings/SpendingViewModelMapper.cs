using AutoMapper;
using EWallet.BusinessLogic.Implementation.Spendings.ViewModel;
using EWallet.Entities.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Spendings.Mappings
{
    public class SpendingViewModelMapper : Profile 
    {
        public SpendingViewModelMapper() {

            CreateMap<Spending, SpendingViewModel>()
                .ForMember(a => a.Id, a => a.MapFrom(s => s.Id))
                .ForMember(a => a.Amount, a => a.MapFrom(s => s.Amount))
                .ForMember(a => a.SpendingCategoryId, a => a.MapFrom(s => s.SpendingCategoryId))
                .ForMember(a => a.SpendingDescription, a => a.MapFrom(s => s.SpendingDescription))
                .ForMember(a => a.SpendingCategoryName, a => a.MapFrom(s => s.SpendingCategory.CategoryName))
                .ForMember(a => a.RecurrenceTypeName, a => a.MapFrom(s => s.RecurrenceType.RecurrenceTypeName))

                ;


        }
    }
}
