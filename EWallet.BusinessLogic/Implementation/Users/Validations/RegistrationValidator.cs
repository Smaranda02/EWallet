using EWallet.DataAccess;
using EWallet.BusinessLogic.Implementation.Users.ViewModel;
using FluentValidation;

namespace EWallet.BusinessLogic.Implementation.Users.Validations
{
    public class RegistrationValidator : AbstractValidator<RegistrationViewModel>
    {

        public RegistrationValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Please enter your email address")
                .EmailAddress().WithMessage("Invalid email format")
                .Must(NotAlreadyExist).WithMessage("Email already exists");

            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Please enter your first name.")
                .Must(firstName => firstName.All(char.IsLetter)).WithMessage("First name should contain only letters")
                .Length(3, 40).WithMessage("Minimum length is 3");

            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Please enter your last name.")
                .Must(firstName => firstName.All(char.IsLetter)).WithMessage("First name should contain only letters")
                .Length(3, 40).WithMessage("Minimum length is 3");


            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Enter a password")
                .MinimumLength(3).WithMessage("Password must be at least 3 characters long.");


            RuleFor(r => r.ConfirmedPassword)
               .NotEmpty().WithMessage("Confirm your password")
                .Equal(r => r.Password).WithMessage("Passwords do not match");

            RuleFor(r => r.BirthDate)
                .NotEmpty().WithMessage("Please enter your birth date");
        }


        public bool NotAlreadyExist(string email)
        {
            return true;
        }
    }
}
