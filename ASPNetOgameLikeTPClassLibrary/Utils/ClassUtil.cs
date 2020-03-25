using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Utils
{
    public static class ClassUtil
    {
        public const String CONCRET_BUILDINGS = "ASPNetOgameLikeTPClassLibrary.Entities.ConcretBuildings.";

        public static object CreateInstance(string strFullyQualifiedName)
        {
            Type t = Type.GetType(strFullyQualifiedName);
            return Activator.CreateInstance(t);
        }
    }
}
