﻿using ASPNetOgameLikeTPClassLibrary.Entities;
using ASPNetOgameLikeTPClassLibrary.Entities.Configurations;
using ASPNetOgameLikeTPClassLibrary.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNetOgameLikeTP.Utils
{
    public class ConfigurationsUtil
    {
        private static ConfigurationsUtil _instance;
        static readonly object instanceLock = new object();

        private ConfigurationsUtil()
        {
            Configuration = JsonConvert.DeserializeObject<GameConfiguration>(FileUtil.ReadServerFile("~/Content/Configurations/GameConfigurations.txt"));
        }

        public static ConfigurationsUtil Instance
        {
            get
            {
                if (_instance == null) //Les locks prennent du temps, il est préférable de vérifier d'abord la nullité de l'instance.
                {
                    lock (instanceLock)
                    {
                        if (_instance == null) //on vérifie encore, au cas où l'instance aurait été créée entretemps.
                            _instance = new ConfigurationsUtil();
                    }
                }
                return _instance;
            }
        }

        public GameConfiguration Configuration { get; private set; }

        public List<Building> PlanetBuildings(GlobalPlanetConfiguration globalPlanetConfiguration)
        {
            List<Building> result = new List<Building>();
            foreach (var buildingId in globalPlanetConfiguration.BuildingsIds)
            {
                result.Add(Configuration.ResourceGenerators.ElementAt(buildingId));
            }

            return result;
        }

        public List<Resource> PlanetResources(GlobalPlanetConfiguration globalPlanetConfiguration)
        {
            List<Resource> result = new List<Resource>();
            foreach (var resourceId in globalPlanetConfiguration.ResourcesIds)
            {
                result.Add(Configuration.Resources.ElementAt(resourceId));
            }

            return result;
        }

        public static String GameDefaultConfString()
        {
            String result = "";

            GameConfiguration conf = new GameConfiguration();
            conf.ResourceGenerators.Add(new ResourceGenerator()
            {
                Name = "générateur d'oxygène",
                EnergyCostFuncString = "1|0|1|0",
                OxygenCostFuncString = "200|12|20",
                SteelCostFuncString = "1000|8|20",
                UraniumCostFuncString = "1500|20|20",
                EnergyGenFuncString = "",
                OxygenGenFuncString = "20|2|5",
                SteelGenFuncString = "",
                UraniumGenFuncString = "",
            }) ;
            conf.ResourceGenerators.Add(new ResourceGenerator() 
            { 
                Name = "centrale à énergie",
                EnergyCostFuncString = "1|0|1|0",
                OxygenCostFuncString = "1|200|10|20",
                SteelCostFuncString = "1|100|8|20",
                UraniumCostFuncString = "3|0|0|200|20|20",
                EnergyGenFuncString = "3|0|1|10",
                OxygenGenFuncString = "",
                SteelGenFuncString = "",
                UraniumGenFuncString = "",
            });
            conf.ResourceGenerators.Add(new ResourceGenerator() 
            { 
                Name = "générateur d'acier",
                EnergyCostFuncString = "1|0|1|0",
                OxygenCostFuncString = "2|0|0|300|6|50",
                SteelCostFuncString = "100|8|20",
                UraniumCostFuncString = "7|0|0|200|12|20",
                EnergyGenFuncString = "",
                OxygenGenFuncString = "",
                SteelGenFuncString = "10|2|1",
                UraniumGenFuncString = "",
            });
            conf.ResourceGenerators.Add(new ResourceGenerator() 
            { 
                Name = "générateur d'uranium",
                EnergyCostFuncString = "1|0|1|0",
                OxygenCostFuncString = "200|2|20",
                SteelCostFuncString = "100|3|20",
                UraniumCostFuncString = "",
                EnergyGenFuncString = "",
                OxygenGenFuncString = "",
                SteelGenFuncString = "",
                UraniumGenFuncString = "7|0|0|0|1|2",
            });

            conf.Resources.Add(new Resource() { Name = ResourceNames.Energy.GetName() });
            conf.Resources.Add(new Resource() { Name = ResourceNames.Oxygen.GetName() });
            conf.Resources.Add(new Resource() { Name = ResourceNames.Steel.GetName() });
            conf.Resources.Add(new Resource() { Name = ResourceNames.Uranium.GetName() });

            result = JsonConvert.SerializeObject(conf);
            Console.WriteLine(result);

            return result;
        }
    }
}