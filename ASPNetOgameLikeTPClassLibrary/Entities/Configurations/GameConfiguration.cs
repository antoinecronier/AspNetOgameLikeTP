using ASPNetOgameLikeTPClassLibrary.Entities.ConcretBuildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Entities.Configurations
{
    public class GameConfiguration
    {
        public List<Building> Buildings { get; set; }

        public List<Resource> Resources { get; set; }

        public GlobalGameConfiguration GlobalGameConfiguration { get; set; }

        public GlobalPlanetConfiguration GlobalPlanetConfiguration { get; set; }
    }
}
