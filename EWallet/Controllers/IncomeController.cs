using EWallet.BusinessLogic.Implementation.Incomes.ViewModel;
using EWallet.DataAccess;
using EWallet.WebApp.Code.Base;
using Microsoft.AspNetCore.Mvc;
using EWallet.BusinessLogic.Implementation.Incomes;
using AutoMapper;
using EWallet.Entities.Entities;
using EWallet.BusinessLogic.Implementation.Incomes.Validations;
using EWallet.Code.ExtensionMethods;
using FluentValidation;
using EWallet.BusinessLogic.Implementation.Spendings;
using EWallet.DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;

namespace EWallet.Controllers
{
    [Authorize(Roles = "User")]
    public class IncomeController : BaseController
    {
        private readonly IncomeService _incomeService;
        private readonly CreateIncomeValidator _createIncomeValidator;
        private readonly EditIncomeValidator _editIncomeValidator;

        public IncomeController(ControllerDependencies dependencies, IncomeService incomesService, CreateIncomeValidator createIncomeValidator,
            EditIncomeValidator editIncomeValidator)
           : base(dependencies)
        {
            _incomeService = incomesService;
            _createIncomeValidator = createIncomeValidator;
            _editIncomeValidator = editIncomeValidator;

        }


        public async Task<IActionResult> Index()
        {
            var allIncomes = await _incomeService.GetIncomes();
            var recurrenceTypes = await _incomeService.GetRecurrenceTypesAsSelectListItems();
            
            return View(new DisplayIncomesViewModel
            {
                IncomeViewModels = allIncomes,
                RecurrenceTypes=recurrenceTypes,
            });
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateIncomeViewModel model)
        {
            if (model == null)
            {
                return View("Error_NotFound");
            }


            var validateResult = _createIncomeValidator.Validate(model);
            if (!validateResult.IsValid)
            {
                return Ok(validateResult.GetErrorsResult());
            }
            else
            {
                await _incomeService.CreateIncome(model);
                return Ok(new { });
            };
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var income = await _incomeService.GetIncomeToEdit(id);
            return Ok(income);
        }


        [HttpPost("Income/Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _incomeService.DeleteIncome(id);
            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var income = await _incomeService.GetIncomeToEdit(id);
            return Ok(income);
        }

        [HttpGet]
        public async Task<IActionResult> GetLatestIncomes()
        {
            var incomes = await _incomeService.GetLatestIncomes();
            return Ok(incomes);
        }

        [HttpGet]
        public async Task<IActionResult> GetUpcomingIncomes()
        {
            var incomes = await _incomeService.GetUpcomingIncomes();
            return Ok(incomes);
        }



        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] EditIncomeViewModel model)
        {
            var validateResult = _editIncomeValidator.Validate(model);
            if (!validateResult.IsValid)
            {
                return Ok(validateResult.GetErrorsResult());
            }
            else
            {
                await _incomeService.EditIncome(model);

                return Ok(new { });
            };
            
        }




    }
}
