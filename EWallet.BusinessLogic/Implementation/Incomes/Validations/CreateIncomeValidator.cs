using EWallet.BusinessLogic.Implementation.Incomes.ViewModel;
using EWallet.Common.Enums;
using EWallet.DataAccess;
using EWallet.DataAccess.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Incomes.Validations
{
    public class CreateIncomeValidator : AbstractValidator<CreateIncomeViewModel>
    {
        public CreateIncomeValidator(UnitOfWork unitOfWork) {

            RuleFor(r => r.IncomeDescription)
                .NotEmpty().WithMessage("Please enter a description");

            RuleFor(r => r.IncomeSum)
                .NotEmpty().WithMessage("Please enter an amount")
                .LessThan(99999).WithMessage("The maximum amount allowed is 99999")
                .Custom((incomeSum, context) =>
                {
                    if (incomeSum < 0)
                    {
                        context.AddFailure(context.PropertyName, "The income must be greater than 0");
                    }

                });

            RuleFor(r => r.RecurringNumber)
               .Custom((recurringNumber, context) =>
               {
                   var isValid = true;
                   if (context.InstanceToValidate.RecurrenceTypeId.HasValue)
                   {
                       switch ((RecurrenceTypes)context.InstanceToValidate.RecurrenceTypeId.Value)
                       {
                           case RecurrenceTypes.Weekly:
                               isValid = recurringNumber >= 1 && recurringNumber <= 7;
                               break;
                           case RecurrenceTypes.Monthly:
                               isValid = recurringNumber >= 1 && recurringNumber <= 30;
                               break;
                           case RecurrenceTypes.Yearly:
                               isValid = recurringNumber >= 1 && recurringNumber <= 365;
                               break;
                       }

                       if (!isValid)
                       {
                           context.AddFailure(context.PropertyName, "Incorrect Recurring Number");
                       }
                   }

                   if(!context.InstanceToValidate.RecurrenceTypeId.HasValue && context.InstanceToValidate.RecurringNumber.HasValue)
                       context.AddFailure(context.PropertyName, "Please enter Recurrence Type first");


               });
        }
    }
}
