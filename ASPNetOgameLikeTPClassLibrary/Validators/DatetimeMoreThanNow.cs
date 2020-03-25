using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Validators
{
    public class DatetimeMoreThanNow : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool result = true;
            DateTime dateTime;

            if (DateTime.TryParse(value.ToString(), out dateTime))
            {
                if (DateTime.Compare(dateTime, DateTime.Now) < 0)
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }
    }
}
