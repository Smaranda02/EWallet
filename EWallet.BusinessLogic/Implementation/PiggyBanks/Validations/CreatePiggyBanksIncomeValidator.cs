using EWallet.BusinessLogic.Implementation.PiggyBanks.ViewModel;
using EWallet.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.PiggyBanks.Validations
{
    public class CreatePiggyBanksIncomeValidator : AbstractValidator<PiggyBanksIncomeViewModel>
    {
        public CreatePiggyBanksIncomeValidator(UnitOfWork unitOfWork)
        {


            RuleFor(r => r.AllocatedIncomeAmount)
                  .NotEmpty().WithMessage("Please allocate a valid income sum")
                  .LessThan(99999).WithMessage("The maximum amount allowed is 99999")
                  .Custom((sum, context) =>
                  {
                      if (sum < 0)
                      {
                          context.AddFailure(context.PropertyName, "The income must be greater than 0");
                      }

                  });

        }
    }
}
