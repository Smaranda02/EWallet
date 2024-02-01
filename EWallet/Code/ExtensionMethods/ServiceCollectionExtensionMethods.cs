using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using EWallet.BusinessLogic.Implementation.Users;
using EWallet.BusinessLogic.Implementation.Incomes;
using EWallet.BusinessLogic.Implementation.Spendings;
using System.Security.Claims;
using EWallet.BusinessLogic;
using EWallet.BusinessLogic.Implementation.Users.ViewModel;
using EWallet.WebApp.Code.Base;
using System;
using System.Linq;
using EWallet.BusinessLogic.Implementation.PiggyBanks;
using EWallet.BusinessLogic.Implementation.PiggyBanks.Validations;
using EWallet.BusinessLogic.Implementation.Incomes.Validations;
using EWallet.BusinessLogic.Implementation.Spendings.Validations;
using EWallet.BusinessLogic.Implementation.Transactions;
using EWallet.BusinessLogic.Implementation.Users.Validations;
using EWallet.BusinessLogic.Implementation.VwSpendingCategoriesCount;

namespace EWallet.WebApp.Code.ExtensionMethods
{
    public static class ServiceCollectionExtensionMethods
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddScoped<ControllerDependencies>();

            return services;
        }

        public static IServiceCollection AddEWalletBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<ServiceDependencies>();
            services.AddScoped<UserService>();
            services.AddScoped<IncomeService>();
            services.AddScoped<SpendingService>();
            services.AddScoped<PiggyBankService>();
            services.AddScoped<TransactionService>();

            services.AddScoped<CreatePiggyBankValidator>();
            services.AddScoped<EditPiggyBankValidator>();
            services.AddScoped<CreateIncomeValidator>();
            services.AddScoped<CreateSpendingValidator>();
            services.AddScoped<EditIncomeValidator>();
            services.AddScoped<EditSpendingValidator>();
            services.AddScoped<RegistrationValidator>();
            services.AddScoped<LoginValidator>();
            services.AddScoped<CreatePiggyBanksIncomeValidator>();



            return services;
        }

        public static IServiceCollection AddEWalletCurrentUserViewModel(this IServiceCollection services)
        {
            services.AddScoped(s =>
            {
                var accessor = s.GetService<IHttpContextAccessor>();
                var httpContext = accessor.HttpContext;
                if (httpContext.User.Identity.IsAuthenticated)
                {
                    var claims = httpContext.User.Claims;
                    var userId = claims?.FirstOrDefault(c => c.Type == "Id")?.Value;
                    var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                    var username = claims?.FirstOrDefault(c => c.Type == "Username")?.Value;
                    var role = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                    var firstName = claims?.FirstOrDefault(c => c.Type == "FirstName")?.Value;
                    var lastName = claims?.FirstOrDefault(c => c.Type == "LastName")?.Value;

                    return new CurrentUserViewModel
                    {
                        Id = int.Parse(userId),
                        IsAuthenticated = httpContext.User.Identity.IsAuthenticated,
                        Email = email,
                        Username=username,
                        Role=role,
                        FirstName=firstName,
                        LastName=lastName,
                    };
                }
                return new CurrentUserViewModel();
            });

            return services;
        }
    }
}


