using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Validators
{
    public class DoubleValidator : ValidationAttribute
    {
        private double min;
        private double max;

        public DoubleValidator(double min, double max)
        {
            this.min = min;
            this.max = max;
        }

        public override bool IsValid(object value)
        {
            bool result = true;
            double parsedDouble;
            if (value != null)
            {
                if (double.TryParse(value.ToString(), out parsedDouble))
                {
                    if (parsedDouble < min || parsedDouble > max)
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            
            return result;
        }
    }
}
