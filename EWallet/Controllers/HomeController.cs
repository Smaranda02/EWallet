using EWallet.BusinessLogic;
using EWallet.BusinessLogic.Implementation.Home.ViewModel;
using EWallet.BusinessLogic.Implementation.Spendings;
using EWallet.BusinessLogic.Implementation.Transactions;
using EWallet.BusinessLogic.Implementation.Users;
using EWallet.BusinessLogic.Implementation.VwSpendingCategoriesCount;
using EWallet.DataAccess.Enums;
using EWallet.WebApp.Code.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EWallet.Controllers
{
    [Authorize(Roles = "User")]
    public class HomeController : BaseController
    {
        private readonly TransactionService _transactionService;
        private readonly SpendingService _spendingService;

        public HomeController(ControllerDependencies dependencies, TransactionService transactionService,
            SpendingService spendingService) : base(dependencies)
        {
            _transactionService = transactionService;
            _spendingService = spendingService;
 
        }

        public async Task<IActionResult> Index()
        {
            var incomes = await _transactionService.GetNumberOfIncomes();
            var spendings = await _transactionService.GetNumberOfSpendings();
            var savings = await _transactionService.GetNumberOfSavings();
            var categoryAppearances = await _spendingService.GetSpendingCategoriesCount();
            var homeVM = new HomeViewModel
            {
                IncomesNumber = incomes,
                SpendingsNumber = spendings,
                SavingsNumber = savings,
                SpendingCategoriesCount=categoryAppearances,
            };

            return View(homeVM);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}