using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Utils
{
    public static class MathUtil
    {
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
