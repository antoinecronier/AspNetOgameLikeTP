using ASPNetOgameLikeTP.Data;
using ASPNetOgameLikeTP.Utils;
using ASPNetOgameLikeTPClassLibrary.Entities;
using ASPNetOgameLikeTPClassLibrary.Entities.Configurations;
using ASPNetOgameLikeTPClassLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNetOgameLikeTP.Builders
{
    public class GameBuilder
    {
        private const string DEFAULT_DISPLAY_RESOURCE = "Planet";
        private GlobalGameConfiguration globalGameConfiguration = null;
        private GlobalPlanetConfiguration globalPlanetConfiguration = null;

        public GameBuilder()
        {

        }

        public List<SolarSystem> BuildOnlySolarSystems()
        {
            List<SolarSystem> solarSystems = new List<SolarSystem>();
            using (var db = new ASPNetOgameLikeTPContext())
            {
                for (int i = 1; i < this.globalGameConfiguration.SolarSystemNb + 1; i++)
                {
                    SolarSystem solarSystem = new SolarSystem();
                    solarSystem.Name = "système solaire " + i;
                    solarSystems.Add(solarSystem);
                }
            }
            return solarSystems;
        }

        public Universe BuildUniverse()
        {
            Universe result = new Universe() { Name = "universe 1" };

            List<SolarSystem> solarSystems = new List<SolarSystem>();
            using (var db = new ASPNetOgameLikeTPContext())
            {
                for (int i = 1; i < this.globalGameConfiguration.SolarSystemNb + 1; i++)
                {
                    SolarSystem solarSystem = new SolarSystem();
                    for (int j = 1; j < this.globalGameConfiguration.PlanetsNb + 1; j++)
                    {
                        Planet planet = new Planet();

                        foreach (var item in this.globalPlanetConfiguration.BuildingsIds)
                        {
                            ResourceGenerator buildingTemp = ConfigurationsUtil.Instance.PlanetResourceGenerators(globalPlanetConfiguration).FirstOrDefault(x => x.Id == item);
                            buildingTemp.Print = DEFAULT_DISPLAY_RESOURCE;
                            buildingTemp.Id = null;
                            planet.Buildings.Add(ClassUtil.Copy(buildingTemp));
                        }

                        foreach (var item in this.globalPlanetConfiguration.ResourcesIds)
                        {
                            Resource resourceTemp = ConfigurationsUtil.Instance.PlanetResources(globalPlanetConfiguration).FirstOrDefault(x => x.Id == item);
                            resourceTemp.LastUpdate = DateTime.Now;
                            resourceTemp.LastQuantity = 0;
                            resourceTemp.Print = DEFAULT_DISPLAY_RESOURCE;
                            resourceTemp.Id = null;
                            planet.Resources.Add(ClassUtil.Copy(resourceTemp));
                        }

                        planet.CaseNb = MathUtil.DrawRandom(20*j%300,30*j%300);
                        planet.Name = $"Planet{j}";
                        planet.Print = DEFAULT_DISPLAY_RESOURCE;

                        solarSystem.Planets.Add(planet);
                    }
                    solarSystem.Name = "système solaire " + i;
                    solarSystems.Add(solarSystem);
                }
            }

            result.SolarSystems.AddRange(solarSystems);

            return result;
        }

        public GameBuilderChain1 AddGlobalGameConfiguration(GlobalGameConfiguration globalGameConfiguration)
        {
            this.globalGameConfiguration = globalGameConfiguration;

            return new GameBuilderChain1(this);
        }

        public class GameBuilderChain1
        {
            private GameBuilder gameBuilder;

            public GameBuilderChain1(GameBuilder gameBuilder)
            {
                this.gameBuilder = gameBuilder;
            }

            public GameBuilder AddPlanetsOnSolarSystem(GlobalPlanetConfiguration globalPlanetConfiguration)
            {
                gameBuilder.globalPlanetConfiguration = globalPlanetConfiguration;

                return gameBuilder;
            }
        }
    }
}
