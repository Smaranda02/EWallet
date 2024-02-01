using EWallet.BusinessLogic.Implementation.PiggyBanks.ViewModel;
using FluentValidation;
using EWallet.BusinessLogic.Implementation;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EWallet.DataAccess;

namespace EWallet.BusinessLogic.Implementation.PiggyBanks.Validations
{
    public class EditPiggyBankValidator : AbstractValidator<EditPiggyBankViewModel>
    {

        public EditPiggyBankValidator(UnitOfWork unitOfWork)
        {


            RuleFor(r => r.TargetSum)
                .NotEmpty().WithMessage("Please enter an amount")
                .LessThan(99999).WithMessage("The maximum amount allowed is 99999")
                .Custom((targetSum, context) =>
                {
                    if (targetSum < 0)
                    {
                        context.AddFailure(context.PropertyName, "The income must be greater than 0");
                    }

                });


            RuleFor(r => r.PiggyBankDescription)
                .NotEmpty().WithMessage("Please enter a description");

            RuleFor(r => r.DueDate)
                .NotEmpty().WithMessage("Please enter a date")
                //.Must((DateTime? dt) => dt > DateTime.Now).WithMessage("The date must be in the future");
                ;


            RuleFor(r => r.SavingPriority)
                  .NotEmpty().WithMessage("Please enter a priority")
                  .Custom((priority, context) =>
                  {
                        if (priority < 0)
                        {
                            context.AddFailure(context.PropertyName, "The priority must be greater than 0");
                        }

                  });
        }
    }
}
