using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Common.Exceptions
{
    public class ValidationErrorException : Exception
    {
        public readonly ValidationResult ValidationResult;
        public object? Model;

        public ValidationErrorException(ValidationResult result, object? model = null)
        {
            ValidationResult = result;
            Model = model;
        }
    }
}
