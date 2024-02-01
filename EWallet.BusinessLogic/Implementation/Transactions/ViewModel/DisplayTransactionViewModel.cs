using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Transactions.ViewModel
{
    public class DisplayTransactionViewModel
    {
        public List<TransactionViewModel> TransactionViewModels = new();
        public int PageIndex { get;  set; }
        public int TotalPages { get;  set; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public List<SelectListItem> TansactionCategories { get; set; } = new();

    }
}
