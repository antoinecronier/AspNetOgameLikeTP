using ASPNetOgameLikeTP.Data;
using ASPNetOgameLikeTP.Models;
using ASPNetOgameLikeTP.Utils;
using ASPNetOgameLikeTPClassLibrary.Entities;
using ASPNetOgameLikeTPClassLibrary.Entities.Configurations;
using ASPNetOgameLikeTPClassLibrary.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASPNetOgameLikeTP.Services
{
    public class ResourceService
    {
        public void RefreshResources(GameUserVM vm)
        {
            List<Resource> resourcesGenerate = new List<Resource>();
            foreach (var building in vm.PrincipalPlanet.Buildings)
            {
                if (building is ResourceGenerator)
                {
                    var resourceGenerator = building as ResourceGenerator;
                    this.ApplyResourceConfigurations(resourceGenerator);

                    foreach (var resource in resourceGenerator.ResourceBySecond)
                    {
                        var realResource = vm.PrincipalPlanet.Resources.FirstOrDefault(x => x.Name.Equals(resource.Name));
                        TimeSpan ts = DateTime.Now - realResource.LastUpdate;
                        int calc = (int)((double)resource.LastQuantity.Value * ts.TotalSeconds);
                        if (resourcesGenerate.Any(x => x.Name.Equals(resource.Name)))
                        {
                            var resTmp = resourcesGenerate.FirstOrDefault(x => x.Name.Equals(resource.Name));
                            resTmp.LastQuantity = resTmp.LastQuantity + calc;
                        }
                        else
                        {
                            resourcesGenerate.Add(new Resource() { Name = resource.Name, LastQuantity = calc });
                        }
                    }
                }
            }

            using (var db = new ASPNetOgameLikeTPContext())
            {
                foreach (var resource in vm.PrincipalPlanet.Resources)
                {
                    resource.LastQuantity = resource.LastQuantity + resourcesGenerate.FirstOrDefault(x => x.Name.Equals(resource.Name)).LastQuantity;
                    this.SaveResource(db, resource);
                }
            }
        }

        public bool BuyIt(GameUserVM vm, Building building)
        {
            bool result = true;

            this.ApplyResourceConfigurations(building);
            foreach (var resource in building.NextCost)
            {
                var realResource = vm.PrincipalPlanet.Resources.FirstOrDefault(x => x.Name.Equals(resource.Name));
                if (resource.LastQuantity > realResource.LastQuantity)
                {
                    result = false;
                }
            }

            if (result)
            {
                using (var db = new ASPNetOgameLikeTPContext())
                {
                    foreach (var resource in building.NextCost)
                    {
                        var realResource = vm.PrincipalPlanet.Resources.FirstOrDefault(x => x.Name.Equals(resource.Name));
                        realResource.LastQuantity = realResource.LastQuantity - resource.LastQuantity;
                        this.SaveResource(db, realResource);
                    }
                }
            }

            return result;
        }

        private void SaveResource(ASPNetOgameLikeTPContext db, Resource resource)
        {
            resource.LastUpdate = DateTime.Now;

            db.Resources.Attach(resource);
            db.Entry(resource).State = EntityState.Modified;

            db.SaveChanges();
        }

        private void ApplyResourceConfigurations(Building building)
        {
            // Apply configuration to resource generator
            using (var db = new ASPNetOgameLikeTPContext())
            {
                var resPlanetConf = db.Configurations.Find(ConfigurationKeys.GlobalPlanetConfiguration.GetName());
                var planetConf = JsonConvert.DeserializeObject<GlobalPlanetConfiguration>(resPlanetConf.Data);
                ConfigurationsUtil.Instance.LoadBuildingConf(planetConf, building);
            }
        }
    }
}