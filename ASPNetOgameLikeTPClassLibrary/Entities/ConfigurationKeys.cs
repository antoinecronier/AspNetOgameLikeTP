using ASPNetOgameLikeTPClassLibrary.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Entities
{
    public enum ConfigurationKeys
    {
        [EnumNaming(Name = "GlobalGameConfiguration")]
        GlobalGameConfiguration,

        [EnumNaming(Name = "GlobalPlanetConfiguration")]
        GlobalPlanetConfiguration,
    }
}
