using EWallet.BusinessLogic.Implementation.Users.ViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Users.Validations
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Please enter your email address")
                .EmailAddress().WithMessage("Invalid email format");


            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Please enter your password");


        }
    }
}
