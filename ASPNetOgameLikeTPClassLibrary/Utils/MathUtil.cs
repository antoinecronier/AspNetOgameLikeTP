using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Utils
{
    public static class MathUtil
    {
        public static Func<int?, int?> MathFuncFromString(string value)
        {
            Func<int?, int?> result = (int? x) => { return 0; };
            int?[] terms = value.Split('|').Select((x) =>
            {
                int? data = null;
                if (!String.IsNullOrEmpty(x))
                {
                    data = int.Parse(x);
                }

                return data;
            }).ToArray();
            if (terms != null)
            {
                if (terms.Length == 3)
                {
                    result = (int? x) => { return (terms[0] * (x / terms[1])) + terms[2]; };
                }
                else if (terms.Length == 4)
                {
                    result = (int? x) => { return (terms[0] * x) + (terms[1] * (x / terms[2])) + terms[3]; };
                }
                else if (terms.Length == 5)
                {
                    result = (int? x) => { return (terms[0] * (x * x)) + (terms[1] * x) + (terms[2] * (x / terms[3])) + terms[4]; };
                }
                else if (terms.Length == 6)
                {
                    result = (int? x) => { return (terms[0] * (x * x * x)) + (terms[1] * (x * x)) + (terms[2] * x) + (terms[3] * (x / terms[4])) + terms[5]; };
                }
            }

            return result;
        }

        public static int? FactorialExpression(Func<int?, int?> expression, int? level)
        {
            int? result = 0;

            for (int i = 1; i < level + 1; i++)
            {
                result += expression.Invoke(i);
            }

            return result;
        }

        public static int? DrawRandom(int min, int max)
        {
            int? result = null;

            Random rand = new Random();
            result = rand.Next(min, max);

            return result;
        }
    }
}
