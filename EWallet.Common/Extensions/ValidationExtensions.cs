using FluentValidation.Results;
using EWallet.Common.Exceptions;
using FluentValidation;

namespace EWallet.Common.Extensions
{
    public static class ValidationExtensions
    {
        public static void ThenThrow<T>(this ValidationResult result, T model, Func<T, T> modelHydrationFunc)
        {
            if (!result.IsValid)
            {
                throw new ValidationErrorException(result, modelHydrationFunc(model));
            }
        }
        public static async Task ThenThrow<T>(this ValidationResult result, T model, Func<T, Task<T>> modelHydrationFunc)
        {
            if (!result.IsValid)
            {
                throw new ValidationErrorException(result, await modelHydrationFunc(model));
            }
        }
        public static void ThenThrow<T>(this ValidationResult result, T model)
        {
            if (!result.IsValid)
            {
                throw new ValidationErrorException(result, model);
            }
        }
        public static void ThenThrow<T>(this ValidationResult result)
        {
            if (!result.IsValid)
            {
                throw new ValidationErrorException(result);
            }
        }
    }
}
