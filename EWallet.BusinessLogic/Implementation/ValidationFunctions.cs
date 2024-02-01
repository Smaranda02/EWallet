using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace EWallet.BusinessLogic.Implementation
{
    public static class ValidationFunctions
    {
        public static bool IsInFuture(DateTime date)
        {
            return date > DateTime.Now;
        }

        public static bool BeNumeric(string input)
        {
            return !string.IsNullOrEmpty(input) && Regex.IsMatch(input, "^[0-9]+$");
        }

    }
}
