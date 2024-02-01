using Microsoft.AspNetCore.Mvc;
using EWallet.Entities.Entities;
using EWallet.BusinessLogic.Implementation.Users.ViewModel;
using EWallet.WebApp.Code.Base;
using EWallet.BusinessLogic.Implementation;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using EWallet.BusinessLogic.Implementation.Users;
using AutoMapper;
using EWallet.DataAccess;
using EWallet.BusinessLogic.Implementation.PiggyBanks;
using EWallet.DataAccess.Enums;
using EWallet.Code.ExtensionMethods;
using EWallet.BusinessLogic.Implementation.Users.Validations;

namespace EWallet.Controllers
{
    public class UserController : BaseController
    {

        private readonly UserService userService;
        private readonly PiggyBankService _piggyBankService;
        private readonly RegistrationValidator _registrationValidator;
        private readonly LoginValidator _loginValidator;


        public UserController(ControllerDependencies dependencies, UserService userService_,
            PiggyBankService piggyBankService
            , RegistrationValidator registrationValidator, LoginValidator loginValidator
            )
           : base(dependencies)
        {
            this.userService = userService_;
            _piggyBankService = piggyBankService;
            _registrationValidator = registrationValidator;
            _loginValidator=loginValidator;
        }


        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var model = await userService.CreateRegistrationVMAndDropdownValues();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            if (model == null)
            {
                return View("Error_NotFound");
            }

            var validateResult = _registrationValidator.Validate(model, ModelState);
            if (!validateResult.IsValid)
            {
                return View(model);
            }
            else
            {
                await userService.RegisterNewUser(model);

                return RedirectToAction("Index", "Home");
            };

        }



        [HttpGet]
        public IActionResult RegisterAdvisor()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            var user = await userService.GetUserViewModelByEmailAndPassword(model.Email, model.Password);

            if (!user.IsAuthenticated)
            {
                model.AreCredentialsInvalid = true;
                return View(model);
            }

            await LogIn(user);

            return RedirectToAction("Index", "Home");
            
        }


        private async Task LogIn(CurrentUserViewModel user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, Enum.GetName(typeof(Roles), int.Parse(user.Role))),
                new Claim("MyCustomClaim", "test123"),
                new Claim("LastName", user.LastName),
                new Claim("FirstName", user.FirstName),
                //new Claim("BirthDate", user.BirthDate),
                new Claim("Username", user.Username),

            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                    scheme: "EWalletCookies",
                    principal: principal);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await LogOut();

            return RedirectToAction("Login", "User");
        }

        private async Task LogOut()
        {
            await HttpContext.SignOutAsync(scheme: "EWalletCookies");
        }


        [HttpGet]
        public async Task<IActionResult> GetCurrentBalance()
        {
            var balance = await userService.GetCurrentBalance();      
            return Ok(balance);
        }

        [HttpGet]
        public async Task<IActionResult> GetTotalSavings()
        {
            var savings = await _piggyBankService.GetTotalSavings(CurrentUserViewModel.Id);
            return Ok(savings);
        }

        [HttpPost]
        public async Task<IActionResult> AddFriend([FromBody] string username)
        {

            var errors = await userService.AddFriend(username);
            return Ok(errors.Errors);
        }

        [HttpGet]
        public async Task<IActionResult> GetFriends()
        {
            var friends = await userService.GetFriends();
            
            return Ok(friends);
        }

        [HttpGet]
        public async Task<IActionResult> GetUpcomingBirthdays()
        {
            var users = await userService.GetUpcomingBirthdays();

            return Ok(users);
        }
    }
}
