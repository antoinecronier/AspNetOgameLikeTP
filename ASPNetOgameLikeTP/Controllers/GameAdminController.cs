using ASPNetOgameLikeTP.Builders;
using ASPNetOgameLikeTP.Data;
using ASPNetOgameLikeTP.Models;
using ASPNetOgameLikeTP.Utils;
using ASPNetOgameLikeTPClassLibrary.Entities;
using ASPNetOgameLikeTPClassLibrary.Entities.Configurations;
using ASPNetOgameLikeTPClassLibrary.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNetOgameLikeTP.Controllers
{
    public class GameAdminController : Controller
    {
        private ASPNetOgameLikeTPContext db = new ASPNetOgameLikeTPContext();

        public ActionResult Configure()
        {
            GameAdminVM vm = new GameAdminVM();
            var resGlobalConf = db.Configurations.Find(ConfigurationKeys.GlobalGameConfiguration.GetName());
            var resPlanetConf = db.Configurations.Find(ConfigurationKeys.GlobalPlanetConfiguration.GetName());

            GlobalGameConfiguration globalConf = new GlobalGameConfiguration();
            GlobalPlanetConfiguration planetConf = new GlobalPlanetConfiguration();

            if (resGlobalConf != null && resPlanetConf != null)
            {
                globalConf = JsonConvert.DeserializeObject<GlobalGameConfiguration>(resGlobalConf.Data);
                planetConf = JsonConvert.DeserializeObject<GlobalPlanetConfiguration>(resPlanetConf.Data);
            }
            else
            {
                resGlobalConf = new Configuration()
                {
                    Key = ConfigurationKeys.GlobalGameConfiguration.GetName(),
                    Data = JsonConvert.SerializeObject(vm.GlobalGameConfiguration)
                };
                resPlanetConf = new Configuration()
                {
                    Key = ConfigurationKeys.GlobalPlanetConfiguration.GetName(),
                    Data = JsonConvert.SerializeObject(vm.GlobalPlanetConfiguration)
                };

                db.Configurations.Add(resGlobalConf);
                db.Configurations.Add(resPlanetConf);

                db.SaveChanges();
            }

            vm.GlobalGameConfiguration = globalConf;
            vm.GlobalPlanetConfiguration = planetConf;
            vm.Resources = ConfigurationsUtil.Instance.Configuration.Resources;
            vm.Buildings = ConfigurationsUtil.Instance.Configuration.ResourceGenerators.ToList<Building>();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Configure(GameAdminVM vm)
        {
            if (ModelState.IsValid)
            {
                Configuration globalConf = new Configuration() 
                { 
                    Key = ConfigurationKeys.GlobalGameConfiguration.GetName(), 
                    Data = JsonConvert.SerializeObject(vm.GlobalGameConfiguration)
                };
                Configuration planetConf = new Configuration()
                {
                    Key = ConfigurationKeys.GlobalPlanetConfiguration.GetName(),
                    Data = JsonConvert.SerializeObject(vm.GlobalPlanetConfiguration)
                };

                db.Configurations.Attach(globalConf);
                db.Configurations.Attach(planetConf);

                db.Entry(globalConf).State = EntityState.Modified;
                db.Entry(planetConf).State = EntityState.Modified;

                db.SaveChanges();

                GameBuilder builder = new GameBuilder();
                List<SolarSystem> solarSystems = builder.AddGlobalGameConfiguration(vm.GlobalGameConfiguration).AddPlanetsOnSolarSystem(vm.GlobalPlanetConfiguration).BuildAll();

                if (ValidationUtil.ValidateObject(solarSystems))
                {
                    try
                    {
                        db.ClearDatabase();
                        db.SolarSystems.AddRange(solarSystems);
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                        Console.WriteLine("error : " + error.PropertyName);
                        Console.WriteLine("message : " + error.ErrorMessage);
                        this.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                        return ReturnView(vm);
                    }
                }

                return Redirect("/Home");
            }
            else
            {
                return ReturnView(vm);
            }
        }

        private ActionResult ReturnView(GameAdminVM vm)
        {
            var globalConf = JsonConvert.DeserializeObject<GlobalGameConfiguration>(db.Configurations.Find(ConfigurationKeys.GlobalGameConfiguration.GetName()).Data);
            var planetConf = JsonConvert.DeserializeObject<GlobalPlanetConfiguration>(db.Configurations.Find(ConfigurationKeys.GlobalPlanetConfiguration.GetName()).Data);
            vm.GlobalGameConfiguration = globalConf;
            vm.GlobalPlanetConfiguration = planetConf;
            vm.Resources = ConfigurationsUtil.Instance.Configuration.Resources;
            vm.Buildings = ConfigurationsUtil.Instance.Configuration.ResourceGenerators.ToList<Building>();

            return View(vm);
        }
    }
}
