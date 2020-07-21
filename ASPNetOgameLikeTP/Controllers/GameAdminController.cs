using ASPNetOgameLikeTP.Builders;
using ASPNetOgameLikeTP.Data;
using ASPNetOgameLikeTP.Models;
using ASPNetOgameLikeTP.Utils;
using ASPNetOgameLikeTPClassLibrary.Comparers;
using ASPNetOgameLikeTPClassLibrary.Entities;
using ASPNetOgameLikeTPClassLibrary.Entities.Configurations;
using ASPNetOgameLikeTPClassLibrary.Extensions;
using ASPNetOgameLikeTPClassLibrary.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
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
            ClassUtil.IdsUpdater(vm.Resources);
            vm.Buildings = ConfigurationsUtil.Instance.Configuration.ResourceGenerators.ToList<Building>();
            ClassUtil.IdsUpdater(vm.Buildings);

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
                Universe universe = builder.AddGlobalGameConfiguration(vm.GlobalGameConfiguration).AddPlanetsOnSolarSystem(vm.GlobalPlanetConfiguration).BuildUniverse();

                if (ValidationUtil.ValidateObject(universe))
                {
                    try
                    {
                        db.ClearDatabase();
                        db.Universes.Add(universe);
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
            ClassUtil.IdsUpdater(vm.Resources);
            vm.Buildings = ConfigurationsUtil.Instance.Configuration.ResourceGenerators.ToList<Building>();
            ClassUtil.IdsUpdater(vm.Buildings);

            return View(vm);
        }

        public ActionResult Printables()
        {
            PrintableVM vm = new PrintableVM();
            vm.Resources = db.Resources.ToList().Distinct(new ResourceComparer()).ToList();
            vm.ResourceGenerators = db.Buildings.ToList().OfType<ResourceGenerator>().Distinct(new ResourceGeneratorComparer()).ToList();
            vm.Images = Directory.GetFiles(HostingEnvironment.MapPath("~/Content/Drawables/")).Select(x => x.Split('.')[0]).Select(x => x.Substring(x.LastIndexOf('\\') + 1, x.Length - x.LastIndexOf('\\') - 1)).ToList();

            List<String> images = new List<string>();
            for (int i = 0; i < vm.Images.Count; i++)
            {
                var image = vm.Images.ElementAt(i);
                images.Add(ClassUtil.ImgPath(vm.Images.ElementAt(i)));
            }

            vm.Images = images;

            return View(vm);
        }

        [HttpPost]
        public ActionResult Printables(PrintableUpdateVM item)
        {
            switch (item.Type)
            {
                case 1:
                    var resources = db.Resources.Where(x => x.Name == item.Name).ToList();
                    foreach (var resource in resources)
                    {
                        resource.Print = item.Print;
                    }
                    db.SaveChanges();
                    break;
                case 2:
                    var resourceGenerators = db.Buildings.Where(x => x.Name == item.Name).OfType<ResourceGenerator>().ToList();
                    foreach (var resourceGenerator in resourceGenerators)
                    {
                        resourceGenerator.Print = item.Print;
                    }
                    db.SaveChanges();
                    break;
                default:
                    break;
            }

            return RedirectToRoute("Default");
        }
    }
}
