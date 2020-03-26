using ASPNetOgameLikeTP.Data;
using ASPNetOgameLikeTP.Utils;
using ASPNetOgameLikeTPClassLibrary.Entities;
using ASPNetOgameLikeTPClassLibrary.Entities.Configurations;
using ASPNetOgameLikeTPClassLibrary.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNetOgameLikeTP.Builders
{
    public class PlanetBuilder
    {
        private string name;
        private int? caseNb;

        public PlanetBuilder Name(String name)
        {
            this.name = name;
            return this;
        }

        public PlanetBuilder CaseNb(int? caseNb)
        {
            this.caseNb = caseNb;
            return this;
        }

        public Planet Build()
        {
            Planet result = new Planet();

            result.Name = this.name;
            result.CaseNb = this.caseNb;
            using (var db = new ASPNetOgameLikeTPContext())
            {
                var resPlanetConf = db.Configurations.Find(ConfigurationKeys.GlobalPlanetConfiguration.GetName());
                var planetConf = JsonConvert.DeserializeObject<GlobalPlanetConfiguration>(resPlanetConf.Data);
                result.Buildings = ConfigurationsUtil.Instance.PlanetResourceGenerators(planetConf).ToList<Building>();
                result.Resources = ConfigurationsUtil.Instance.PlanetResources(planetConf);
            }

            return result;
        }
    }
}