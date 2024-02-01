using AutoMapper;
using EWallet.BusinessLogic.Implementation.VwSpendingCategoriesCount.ViewModel;
using EWallet.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.VwSpendingCategoriesCount.Mapping
{
    public class VwSpendingCategoriesCountMapper : Profile
    {
        public VwSpendingCategoriesCountMapper()
        {
            CreateMap<Entities.Entities.VwSpendingCategoriesCount, VwSpendingCategoriesCountViewModel>()

                .ForMember(a => a.SpendingCategoryID, a => a.MapFrom(s => s.SpendingCategoryId))
                .ForMember(a => a.CategoryName, a => a.MapFrom(s => s.CategoryName))
                .ForMember(a => a.Appearances, a => a.MapFrom(s => s.Appearances));


        }
    }
}
