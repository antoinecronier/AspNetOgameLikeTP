using ASPNetOgameLikeTPClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Comparers
{
    public class ResourceComparer : IEqualityComparer<Resource>
    {
        public bool Equals(Resource x, Resource y)
        {
            if (string.Equals(x.Name, y.Name, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(Resource obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
