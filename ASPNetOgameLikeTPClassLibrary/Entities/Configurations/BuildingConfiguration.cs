using ASPNetOgameLikeTPClassLibrary.Entities.ConcretBuildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Entities.Configurations
{
    public class BuildingConfiguration
    {
        public OxygenGenerator OxygenGenerator { get; set; }
        public PowerPlant PowerPlant { get; set; }
        public SteelGenerator SteelGenerator { get; set; }
        public UraniumGenerator UraniumGenerator { get; set; }
    }
}
