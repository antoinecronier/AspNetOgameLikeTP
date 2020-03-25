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
        [EnumNaming(Name = "énergie")]
        Energy,
        [EnumNaming(Name = "oxygène")]
        Oxygen,
        [EnumNaming(Name = "acier")]
        Steel,
        [EnumNaming(Name = "uranium")]
        Uranium
    }
}
