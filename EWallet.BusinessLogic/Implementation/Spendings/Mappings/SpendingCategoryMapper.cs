using AutoMapper;
using EWallet.BusinessLogic.Implementation.Spendings.ViewModel;
using EWallet.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Spendings.Mappings
{
    public class SpendingCategoryMapper : Profile
    {
        public SpendingCategoryMapper()
        {
            CreateMap<CreateSpendingCategoryViewModel, SpendingCategory>()
            .ForMember(a => a.CategoryName, a => a.MapFrom(s => s.CategoryName))
            .ForMember(a => a.Id, a => a.Ignore());


        }
    }
}
