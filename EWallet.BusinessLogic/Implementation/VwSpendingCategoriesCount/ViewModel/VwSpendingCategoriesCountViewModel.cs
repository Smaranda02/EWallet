using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.VwSpendingCategoriesCount.ViewModel
{
    public class VwSpendingCategoriesCountViewModel
    {
        public int SpendingCategoryID { get; set; }
        public string CategoryName { get; set; }
        public int Appearances { get; set; }
    }
}
