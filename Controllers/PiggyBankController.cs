
using EWallet.BusinessLogic.Implementation.Incomes;
using EWallet.BusinessLogic.Implementation.Incomes.Validations;
using EWallet.BusinessLogic.Implementation.PiggyBanks;
using EWallet.BusinessLogic.Implementation.PiggyBanks.Validations;
using EWallet.BusinessLogic.Implementation.PiggyBanks.ViewModel;
using EWallet.BusinessLogic.Implementation.Users;
using EWallet.Code;
using EWallet.Code.ExtensionMethods;
using EWallet.Common.Exceptions;
using EWallet.DataAccess.Enums;
using EWallet.WebApp.Code.Base;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EWallet.Controllers
{
    [Authorize(Roles = "User")]
    public class PiggyBankController : BaseController
    {

        private readonly PiggyBankService _piggyBankService;
        private readonly CreatePiggyBankValidator _createPiggyBankValidator;
        private readonly EditPiggyBankValidator _editPiggyBankValidator;
        private readonly CreatePiggyBanksIncomeValidator _createPiggyBanksIncomeValidator;
        private readonly IncomeService _incomesService;
        private readonly UserService _userService;

        public PiggyBankController(ControllerDependencies dependencies
            , PiggyBankService piggyBankService
            , CreatePiggyBankValidator createPiggyBankValidator
            , EditPiggyBankValidator editPiggyBankValidator
            , IncomeService incomesService
            , CreatePiggyBanksIncomeValidator createPiggyBanksIncomeValidator,
            UserService userService) :
            base(dependencies)
        {
            _piggyBankService = piggyBankService;
            _createPiggyBankValidator = createPiggyBankValidator;
            _editPiggyBankValidator = editPiggyBankValidator;
            _incomesService = incomesService;
            _createPiggyBanksIncomeValidator = createPiggyBanksIncomeValidator;
            _userService = userService;
        }



        public async Task<IActionResult> Index()
        {
            var personalPiggyBanks = await _piggyBankService.GetPersonalPiggyBanks();
            var collaborativePiggyBanks = await _piggyBankService.GetCollaborativePiggyBanks();
            var allIncomes = await _incomesService.GetIncomesAsSelectListItems();

            foreach (var piggyBank in personalPiggyBanks)
            {
                var collaborators = await _piggyBankService.GetCollaborators(piggyBank.Id);             
                piggyBank.Collaborators = collaborators;
            }

            return View(new DisplayPiggyBanksViewModel
            {
                CurrentUserId = CurrentUserViewModel.Id,
                PersonalPiggyBanks = personalPiggyBanks,
                CollaborativePiggyBanks = collaborativePiggyBanks,
                Incomes = allIncomes,
            }); 
        }




        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePiggyBankViewModel model)
        {
            if (model == null)
            {
                return View("Error_NotFound");                                                                                                                                                                                          
            }

            ValidationResult validateResultPiggyBanksIncome = new ValidationResult();
            for (int index = 0; index < model.PiggyBanksIncomes.Count; index++)
            {
                var incomeValidationResult = _createPiggyBanksIncomeValidator.Validate(model.PiggyBanksIncomes[index]);
                if (!incomeValidationResult.IsValid)
                {
                    validateResultPiggyBanksIncome.Errors.AddRange(incomeValidationResult.Errors);
                }
            }


            var validateResult = _createPiggyBankValidator.Validate(model);
            if (!validateResult.IsValid || !validateResultPiggyBanksIncome.IsValid)
            {
                validateResult.Errors.AddRange(validateResultPiggyBanksIncome.Errors);
                return Ok(validateResult.GetErrorsResult());
            }
            else
            {
                await _piggyBankService.CreatePiggyBank(model);
                return Ok(new { });
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var piggyBank = await _piggyBankService.GetPiggyBankToEdit(id);
            return Ok(piggyBank);
        }

      

        [HttpPost("PiggyBank/Delete")]
        public async Task<IActionResult> DeletePost( int id)
        {
            await _piggyBankService.DeletePiggyBank(id);
            return Ok(new { });
        }


        [HttpPost("PiggyBank/DeleteCollaborationById")]
        public async Task<IActionResult> DeleteCollaboration(int piggyBankId)
        {
            await _piggyBankService.DeleteCollaboration(piggyBankId);
            return Ok(new {});
        }

        [HttpPost("PiggyBank/DeleteCollaboration")]
        public async Task<IActionResult> DeleteCollaborationPost([FromBody] PiggyBankCollaborationViewModel model)
        {
            await _piggyBankService.DeleteCollaborationPost(model);
            return Ok(new { });
        }


        [HttpGet]
        public async Task<IActionResult> Break(int id)
        {
            var piggyBank = await _piggyBankService.GetPiggyBankToEdit(id);
            return Ok(piggyBank);
        }



        [HttpPost("PiggyBank/Break")]
        public async Task<IActionResult> BreakPost(int id)
        {
            await _piggyBankService.BreakPiggyBank(id);
            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var piggyBank = await _piggyBankService.GetPiggyBankToEdit(id);
            return Ok(piggyBank);
        }



        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] EditPiggyBankViewModel model)
        {
           
            ValidationResult validateResultPiggyBanksIncome = new ValidationResult(); ;
            for (int index = 0; index < model.PiggyBanksIncomes.Count; index++)
            {
                var incomeValidationResult = _createPiggyBanksIncomeValidator.Validate(model.PiggyBanksIncomes[index]);
                if (!incomeValidationResult.IsValid)
                {
                    validateResultPiggyBanksIncome.Errors.AddRange(incomeValidationResult.Errors);
                }
            }

            var validateResult = _editPiggyBankValidator.Validate(model);
            if (!validateResult.IsValid || !validateResultPiggyBanksIncome.IsValid)
            {
                validateResult.Errors.AddRange(validateResultPiggyBanksIncome.Errors);
                return Ok(validateResult.GetErrorsResult());
            }
            else
            {
                await _piggyBankService.EditPiggyBank(model);
                return Ok(new { });
            }

        }




        [HttpGet]
        public async Task<IActionResult> EditCollaborativePiggyBank(int id)
        {
            var piggyBank = await _piggyBankService.GetCollaborativePiggyBankToEdit(id);
            return Ok(piggyBank);
        }





        [HttpPost]
        public async Task<IActionResult> EditCollaborativePiggyBank([FromBody] EditCollaborativePiggyBankViewModel model)
        {
            foreach (var cpb in model.CollaborativePiggyBankIncomes)
            {
                var validateResult = _createPiggyBanksIncomeValidator.Validate(cpb);
                if (!validateResult.IsValid)
                {
                    return Ok(validateResult.GetErrorsResult());
                }
            }

            await _piggyBankService.EditCollaborativePiggyBank(model);
            return Ok(new { });

        }



        [HttpGet]
        public async Task<IActionResult> GetAlmostCompletedPiggyBanks()
        {
            var incomes = await _piggyBankService.GetAlmostCompletedPiggyBanks();
            return Ok(incomes);
        }


        [HttpGet]
        public async Task<IActionResult> GetNearDueDatePiggyBanks()
        {
            var incomes = await _piggyBankService.GetNearDueDatePiggyBanks();
            return Ok(incomes);
        }


        [HttpGet]
        public async Task<IActionResult> GetCollaborators(int piggyBankId)
        {
            var collabs = await _piggyBankService.GetCollaborators(piggyBankId);
            return Ok(collabs);
        }


        [HttpGet]
        public async Task<IActionResult> GetNonCollaborators(int piggyBankId,string name)
        {
            var nonCollabs = await _piggyBankService.GetNonCollaborators(piggyBankId,name ?? "");
            return Ok(nonCollabs);
        }


        [HttpPost]
        public async Task<IActionResult> AddCollaborator([FromBody] PiggyBankCollaborationViewModel model)
        {
            try
            {
                await _piggyBankService.AddCollaborator(model);
            }
            catch (Exception ex)
            {
                var error = new ErrorResultItem()
                {
                    ErrorMessage = "The new collaboration couldn't be introduced"
                };

                return Ok(new ErrorsResult { Errors = new List<ErrorResultItem> { error } });
            }
            
            return Ok(new { });
            
        }


    }
}
