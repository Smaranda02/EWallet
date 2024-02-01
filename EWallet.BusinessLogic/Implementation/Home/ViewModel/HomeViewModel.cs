using EWallet.BusinessLogic.Implementation.VwSpendingCategoriesCount.ViewModel;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Home.ViewModel
{
    public class HomeViewModel
    {
        
        public int SpendingsNumber { get; set; }
        public int IncomesNumber { get; set; }
        public int SavingsNumber { get; set; }

        public List<VwSpendingCategoriesCountViewModel> SpendingCategoriesCount { get; set; }

    }
}
