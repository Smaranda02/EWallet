using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EWallet.Code.ExtensionMethods
{
    public static class ValidationResultExtensionMethods
    {
        public static ValidationResult Validate<T>(this AbstractValidator<T> validator, T model, ModelStateDictionary modelState)
        {
            var validationResult = validator.Validate(model);
            foreach (var error in validationResult.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return validationResult;
        }
        public static ErrorsResult GetErrorsResult(this ValidationResult validationResult)
        {
            return new ErrorsResult
            {
                Errors = validationResult.Errors.Select(e => new ErrorResultItem { PropertyName = e.PropertyName, ErrorMessage = e.ErrorMessage }).ToList()
            };
        }

    }
}
