using ASPNetOgameLikeTP.Data;
using ASPNetOgameLikeTPClassLibrary.Entities;
using ASPNetOgameLikeTPClassLibrary.Entities.Configurations;
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

        public GameBuilder ClearDatabase()
        {
            using (var db = new ASPNetOgameLikeTPContext())
            {
                db.Buildings.RemoveRange(db.Buildings);
                db.Configurations.RemoveRange(db.Configurations);
                db.Planets.RemoveRange(db.Planets);
                db.Resources.RemoveRange(db.Resources);
                db.SolarSystems.RemoveRange(db.SolarSystems);

                db.SaveChanges();
            }

            return this;
        }

        public void ApplyConfig()
        {
            using (var db = new ASPNetOgameLikeTPContext())
            {
                List<SolarSystem> solarSystems = new List<SolarSystem>();
                for (int i = 1; i < this.globalGameConfiguration.SolarSystemNb + 1; i++)
                {
                    SolarSystem solarSystem = new SolarSystem();
                    for (int j = 1; j < this.globalGameConfiguration.PlanetsNb; j++)
                    {
                        Planet planet = new Planet();

                        foreach (var item in this.globalPlanetConfiguration.BuildingsIds)
                        {
                            Building building = db.Buildings.Find(item);
                            building.Id = null;
                            planet.Buildings.Add(building);
                        }

                        foreach (var item in this.globalPlanetConfiguration.ResourcesIds)
                        {
                            Resource resource = db.Resources.Find(item);
                            resource.Id = null;
                            planet.Resources.Add(resource);
                        }

                        solarSystem.Planets.Add(planet);
                    }
                    solarSystems.Add(solarSystem);
                }
                
                db.SaveChanges();
            }
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