using ASPNetOgameLikeTPClassLibrary.Entities;
using ASPNetOgameLikeTPClassLibrary.Entities.ConcretBuildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNetOgameLikeTP.Models
{
    public class BuildingVM
    {
        public ConcretBaseBuilding Building { get; set; }

        public String BuildingType { get; set; }

        public List<NamingForViewModel> BuildingTypes { get; set; } 
            = new List<NamingForViewModel>()
            {
                new NamingForViewModel(){ Display = "Générateur d'oxygène", Value = "OxygenGenerator" },
                new NamingForViewModel(){ Display = "Centrale à énergie", Value = "PowerPlant" },
                new NamingForViewModel(){ Display = "Générateur d'uranium", Value = "UraniumGenerator" },
                new NamingForViewModel(){ Display = "Générateur d'acier", Value = "SteelGenerator" },
            };
    }
}