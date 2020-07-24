using ASPNetOgameLikeTPClassLibrary.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Entities
{
    public enum ResourceNames
    {
        [EnumNaming(Name = "energy")]
        Energy,
        [EnumNaming(Name = "oxygen")]
        Oxygen,
        [EnumNaming(Name = "steel")]
        Steel,
        [EnumNaming(Name = "uranium")]
        Uranium
    }
}
