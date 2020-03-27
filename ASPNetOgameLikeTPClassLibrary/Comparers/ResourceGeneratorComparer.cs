using ASPNetOgameLikeTPClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Comparers
{
    public class ResourceGeneratorComparer : IEqualityComparer<ResourceGenerator>
    {
        public bool Equals(ResourceGenerator x, ResourceGenerator y)
        {
            if (string.Equals(x.Name, y.Name, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(ResourceGenerator obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
