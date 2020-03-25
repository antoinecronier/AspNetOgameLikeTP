using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Entities.Configurations
{
    public class GlobalPlanetConfiguration
    {
        [DisplayName("Type de ressource des planètes :")]
        public List<int> ResourcesIds { get; set; }

        [DisplayName("Batiments disponible par planètes :")]
        public List<int> BuildingsIds { get; set; }
    }
}
