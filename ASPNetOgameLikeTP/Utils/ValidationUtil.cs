using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPNetOgameLikeTP.Utils
{
    public static class ValidationUtil
    {
        public static bool ValidateObject(object item)
        {
            var context = new ValidationContext(item, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(item, context, validationResults, true);

            foreach (var error in validationResults)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return isValid;
        }
    }
}