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

        public static ResourceGenerator Copy(ResourceGenerator item)
        {
            ResourceGenerator result = new ResourceGenerator();
            result.Level = item.Level;
            result.Name = item.Name;
            result.OxygenCostFuncString = item.OxygenCostFuncString;
            result.OxygenGenFuncString = item.OxygenGenFuncString;
            result.SteelCostFuncString = item.SteelCostFuncString;
            result.SteelGenFuncString = item.SteelGenFuncString;
            result.UraniumCostFuncString = item.UraniumCostFuncString;
            result.UraniumGenFuncString = item.UraniumGenFuncString;
            result.EnergyCostFuncString = item.EnergyCostFuncString;
            result.EnergyGenFuncString = item.EnergyGenFuncString;

            return result;
        }

        public static Resource Copy(Resource item)
        {
            Resource result = new Resource();
            result.Name = item.Name;
            result.LastUpdate = item.LastUpdate;
            result.LastQuantity = item.LastQuantity;

            return result;
        }

        public static String ImgPath(String name)
        {
            return "~/Content/Drawables/" + name + ".jpg";
        }
    }
}
