using EWallet.BusinessLogic.Implementation.Spendings;
using EWallet.BusinessLogic.Implementation.Transactions;
using EWallet.BusinessLogic.Implementation.Transactions.ViewModel;
using EWallet.DataAccess.Enums;
using EWallet.WebApp.Code.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Printing;

namespace EWallet.Controllers
{
    [Authorize(Roles = "User")]

    public class TransactionController : BaseController
    {

        private readonly TransactionService _transactionService;


        public TransactionController(ControllerDependencies dependencies, TransactionService transactionService) :
            base(dependencies)
         { 
            _transactionService= transactionService;
        }

        public async Task<IActionResult> Index(int? pageNumber
            ,int? sortOrder, int? sortFilter, int? categoryFilter
            )
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = sortFilter;
            ViewData["CurrentCategory"] = categoryFilter;
            
            var transactionCount = await _transactionService.GetFilteredTransactionsCount(categoryFilter);
            var totalPages = (int)Math.Ceiling(transactionCount / (double)10);

            if (pageNumber > totalPages)
            {
                pageNumber = totalPages;
            }
            var transactionCategories = _transactionService.InitializeTransactionCategories();
            var allTransactions = await _transactionService.GetTransactions(pageNumber ?? 1, 10, sortOrder, sortFilter, categoryFilter);            

            DisplayTransactionViewModel displayTransactionVM = new DisplayTransactionViewModel()
            {
                TansactionCategories = transactionCategories,
                TransactionViewModels=allTransactions,
                PageIndex =pageNumber.GetValueOrDefault(1),
                TotalPages = totalPages

        };

            return View(displayTransactionVM);
          
        }

    }
}
