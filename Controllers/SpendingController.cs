
using EWallet.BusinessLogic.Implementation.Spendings;
using EWallet.BusinessLogic.Implementation.Spendings.Validations;
using EWallet.BusinessLogic.Implementation.Spendings.ViewModel;
using EWallet.Common.Exceptions;
using EWallet.Common.Extensions;
using EWallet.WebApp.Code.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using EWallet.Code.ExtensionMethods;
using EWallet.DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;

namespace EWallet.Controllers
{
    [Authorize(Roles = "User")]
    public class SpendingController : BaseController
    {

        private readonly SpendingService _spendingService;
        private readonly CreateSpendingValidator _createSpendingValidator;
        private readonly EditSpendingValidator _editSpendingValidator;

        public SpendingController(ControllerDependencies dependencies, SpendingService spendingService, CreateSpendingValidator createSpendingValidator,
            EditSpendingValidator editSpendingValidator) :
            base(dependencies)
        {
            _spendingService = spendingService;
            _createSpendingValidator=createSpendingValidator;
            _editSpendingValidator = editSpendingValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var allSpendings = await _spendingService.GetSpendings(pageNumber ?? 1, 5);
            var spendingCategories = await _spendingService.InitializeSpendingCategories();
            var recurrenceTypes= await _spendingService.GetRecurrenceTypesAsSelectListItems();
            var model = new DisplaySpendingViewModel
            {
                SpendingViewModels = allSpendings,
                SpendingCategories = spendingCategories,
                RecurrenceTypes = recurrenceTypes,
                PageIndex = pageNumber.GetValueOrDefault(1),
            };


            if (model.PageIndex == 1)
            {
                return View(model);
            }
            else
            {
                return Ok(model);
            }
            
        }






        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpendingViewModel model)
        {
            if (model == null)
            {
                return View("Error_NotFound");
            }

            var validateResult = _createSpendingValidator.Validate(model);
            if (!validateResult.IsValid)
            {
                return Ok(validateResult.GetErrorsResult());
            }
            else
            {
                await _spendingService.CreateSpending(model);

                return Ok(new { });
            };

           
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var spending = await _spendingService.GetCurrentSpendingToEdit(id);
            return Ok(spending);
        }


        [HttpPost("Spending/Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _spendingService.DeleteSpending(id);
            return RedirectToAction("Index");
        }


     

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            

            var spending = await _spendingService.GetCurrentSpendingToEdit(id);
            return Ok(spending);
        }


        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] EditSpendingViewModel model)
        {
            var validateResult = _editSpendingValidator.Validate(model);
            if (!validateResult.IsValid)
            {
                return Ok(validateResult.GetErrorsResult());
            }
            else
            {
                await _spendingService.EditSpending(model);
                return Ok(new { });
            };
        }



        [HttpPost]
        public async Task<IActionResult> CreateSpendingCategory([FromBody] CreateSpendingCategoryViewModel model)
        {
            if (model == null)
            {
                return View("Error_NotFound");
            }

            await _spendingService.CreateSpendingCategory(model);

            return Ok(new {});
        }


        [HttpGet]
        public async Task<IActionResult> GetUpcomingSpendings()
        {
            var spendings = await _spendingService.GetUpcomingSpendings();
            return Ok(spendings);
        }




    }
}
