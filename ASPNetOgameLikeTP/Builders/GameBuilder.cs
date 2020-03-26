using ASPNetOgameLikeTP.Data;
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

        public List<SolarSystem> Build()
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
                            Building buildingTemp = db.Buildings.Find(item);
                            Building building = Activator.CreateInstance(buildingTemp.GetType()) as Building;
                            building.Name = buildingTemp.Name;
                            building.Level = buildingTemp.Level;
                            planet.Buildings.Add(building);
                        }

                        foreach (var item in this.globalPlanetConfiguration.ResourcesIds)
                        {
                            Resource resourceTemp = db.Resources.Find(item);
                            Resource resource = new Resource();
                            resource.Name = resourceTemp.Name;
                            resource.LastQuantity = resourceTemp.LastQuantity;
                            resource.LastUpdate = resourceTemp.LastUpdate;
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