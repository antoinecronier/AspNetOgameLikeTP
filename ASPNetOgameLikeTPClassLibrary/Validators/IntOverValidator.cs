using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Validators
{
    public class IntOverValidator : ValidationAttribute
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public override bool IsValid(object value)
        {
            bool result = true;
            int parsedInt;
            if (value != null)
            {
                if (int.TryParse(value.ToString(), out parsedInt))
                {
                    if (parsedInt < Min || parsedInt > Max)
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
