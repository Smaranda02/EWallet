using EWallet.Entities.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Spendings.ViewModel
{
    public  class DisplaySpendingViewModel
    {
        public List<SpendingViewModel> SpendingViewModels { get; set; }

        public List<SelectListItem> SpendingCategories { get; set; } = new();

        public List<SelectListItem> RecurrenceTypes{ get; set; } = new();

        public int PageIndex { get; set; }

    }
}
