using ASPNetOgameLikeTPClassLibrary.Entities;
using ASPNetOgameLikeTPClassLibrary.Entities.Configurations;
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
            configuration = JsonConvert.DeserializeObject<GameConfiguration>(FileUtil.ReadServerFile("~/Content/Configurations/GameConfigurations.txt"));
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

        public GameConfiguration configuration { get; private set; }

        public List<Building> Buildings
        {
            get 
            {
                List<Building> result = new List<Building>();
                foreach (var buildingId in configuration.GlobalPlanetConfiguration.BuildingsIds)
                {
                    result.Add(configuration.Buildings.ElementAt(buildingId));
                }
                
                return result; 
            }
        }

        public List<Resource> Resources
        {
            get
            {
                List<Resource> result = new List<Resource>();
                foreach (var resourceId in configuration.GlobalPlanetConfiguration.ResourcesIds)
                {
                    result.Add(configuration.Resources.ElementAt(resourceId));
                }

                return result;
            }
        }
    }
}