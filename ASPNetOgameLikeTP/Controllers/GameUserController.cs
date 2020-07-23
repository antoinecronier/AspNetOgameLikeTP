using ASPNetOgameLikeTP.Builders;
using ASPNetOgameLikeTP.Data;
using ASPNetOgameLikeTP.Models;
using ASPNetOgameLikeTP.Services;
using ASPNetOgameLikeTP.Utils;
using ASPNetOgameLikeTPClassLibrary.Entities;
using ASPNetOgameLikeTPClassLibrary.Entities.Configurations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNetOgameLikeTP.Controllers
{
    public class GameUserController : Controller
    {
        private static GameUserVM vm = new GameUserVM();
        private ResourceService resourceService = new ResourceService();
        public ActionResult GameUserView(GameUserVM vm)
        {
            if (vm == null || vm.PrincipalPlanet == null || vm.Universe == null)
            {
                InitFakeDatas(GameUserController.vm);
            }

            return View(GameUserController.vm);
        }

        private static void InitFakeDatas(GameUserVM vm)
        {
            try
            {
                using (var db = new ASPNetOgameLikeTPContext())
                {
                    Universe universe = db.Universes
                        .Include(x => x.SolarSystems.Select(y => y.Planets.Select(p => p.Resources)))
                        .Include(x => x.SolarSystems.Select(y => y.Planets.Select(p => p.Buildings)))
                        .FirstOrDefault();
                    vm.Universe = universe;
                    vm.PrincipalPlanet = universe.SolarSystems.SelectMany(y => y.Planets).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public ActionResult UpgradeBuilding(long? buildingId)
        {
            Building building = null;
            if (buildingId != null)
            {
                using (var db = new ASPNetOgameLikeTPContext())
                {
                    building = db.Buildings.Find(buildingId);
                    if (this.resourceService.BuyIt(GameUserController.vm, building))
                    {
                        building.Level += 1;
                        db.Entry(building).State = EntityState.Modified;
                        db.SaveChanges();
                        vm.Universe.SolarSystems.SelectMany(x => x.Planets).SelectMany(x => x.Buildings).FirstOrDefault(x => x.Id == buildingId).Level++;
                    }
                }
            }

            return PartialView("~/Views/GameUser/GameUserBuilding.cshtml", building);
        }

        public ActionResult GetPageContent()
        {
            return PartialView("GameUserPageContent", GameUserController.vm);
        }

        public ActionResult ChangePlanet(int planetId)
        {
            GameUserController.vm.PrincipalPlanet = GameUserController.vm.Universe.SolarSystems.SelectMany(y => y.Planets).FirstOrDefault(x => x.Id == planetId);

            return PartialView("GameUserContent", GameUserController.vm);
        }

        public ActionResult PreviousSolarSystem(int solarSystemId)
        {
            SolarSystem ss = GameUserController.vm.Universe.SolarSystems.FirstOrDefault(x => x.Id == solarSystemId);
            int position = GameUserController.vm.Universe.SolarSystems.IndexOf(ss);
            GameUserController.vm.PrincipalPlanet = GameUserController.vm.Universe.SolarSystems.ElementAt(position - 1).Planets.FirstOrDefault();

            return PartialView("GameUserPageContent", GameUserController.vm);
        }

        public ActionResult NextSolarSystem(int solarSystemId)
        {
            SolarSystem ss = GameUserController.vm.Universe.SolarSystems.FirstOrDefault(x => x.Id == solarSystemId);
            int position = GameUserController.vm.Universe.SolarSystems.IndexOf(ss);
            GameUserController.vm.PrincipalPlanet = GameUserController.vm.Universe.SolarSystems.ElementAt(position + 1).Planets.FirstOrDefault();

            return PartialView("GameUserPageContent", GameUserController.vm);
        }

        [ChildActionOnly]
        public ActionResult GetHeadband(GameUserVM vm)
        {
            return PartialView("~/Views/GameUser/GameUserTopHeadband.cshtml", vm);
        }

        [ChildActionOnly]
        public ActionResult GetLeftPanel(GameUserVM vm)
        {
            return PartialView("~/Views/GameUser/GameUserLeftPanel.cshtml", vm);
        }

        [ChildActionOnly]
        public ActionResult GetContent(GameUserVM vm)
        {
            return PartialView("~/Views/GameUser/GameUserContent.cshtml", vm);
        }

        [ChildActionOnly]
        public ActionResult GetResources(List<Resource> resources)
        {
            this.resourceService.RefreshResources(GameUserController.vm);
            return PartialView("~/Views/GameUser/GameUserResources.cshtml", resources);
        }

        [ChildActionOnly]
        public ActionResult GetBuilding(Building building)
        {
            return PartialView("~/Views/GameUser/GameUserBuilding.cshtml", building);
        }
    }
}
