using ASPNetOgameLikeTPClassLibrary.Entities;
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

        public static void IdsUpdater<T>(List<T> items) where T : IDbEntity 
        {
            int i = 0;
            foreach (var item in items)
            {
                item.Id = i;
                i++;
            }
        }
    }
}
