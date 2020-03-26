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

        public List<SolarSystem> BuildAll()
        {
            List<SolarSystem> solarSystems = new List<SolarSystem>();
            using (var db = new ASPNetOgameLikeTPContext())
            {
                for (int i = 1; i < this.globalGameConfiguration.SolarSystemNb + 1; i++)
                {
                    SolarSystem solarSystem = new SolarSystem();
                    for (int j = 1; j < this.globalGameConfiguration.PlanetsNb; j++)
                    {
                        Planet planet = new Planet();

                        foreach (var item in this.globalPlanetConfiguration.BuildingsIds)
                        {
                            ResourceGenerator buildingTemp = ConfigurationsUtil.Instance.PlanetResourceGenerators(globalPlanetConfiguration).ElementAt(item);
                            ResourceGenerator building = new ResourceGenerator();
                            building.Level = buildingTemp.Level;
                            building.Name = buildingTemp.Name;
                            building.OxygenCostFuncString = buildingTemp.OxygenCostFuncString;
                            building.OxygenGenFuncString = buildingTemp.OxygenGenFuncString;
                            building.SteelCostFuncString = buildingTemp.SteelCostFuncString;
                            building.SteelGenFuncString = buildingTemp.SteelGenFuncString;
                            building.UraniumCostFuncString = buildingTemp.UraniumCostFuncString;
                            building.UraniumGenFuncString = buildingTemp.UraniumGenFuncString;
                            building.EnergyCostFuncString = buildingTemp.EnergyCostFuncString;
                            building.EnergyGenFuncString = buildingTemp.EnergyGenFuncString;
                            planet.Buildings.Add(building);
                        }

                        foreach (var item in this.globalPlanetConfiguration.ResourcesIds)
                        {
                            Resource resourceTemp = ConfigurationsUtil.Instance.PlanetResources(globalPlanetConfiguration).ElementAt(item);
                            Resource resource = new Resource();
                            resource.Name = resourceTemp.Name;
                            resource.LastUpdate = DateTime.Now;
                            resource.LastQuantity = 0;
                            planet.Resources.Add(resource);
                        }

                        planet.CaseNb = MathUtil.DrawRandom(20*j%300,30*j%300);

                        solarSystem.Planets.Add(planet);
                    }
                    solarSystem.Name = "système solaire " + i;
                    solarSystems.Add(solarSystem);
                }
            }
            return solarSystems;
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