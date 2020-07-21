using ASPNetOgameLikeTP.Builders;
using ASPNetOgameLikeTP.Data;
using ASPNetOgameLikeTP.Models;
using ASPNetOgameLikeTPClassLibrary.Entities;
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
        public ActionResult GameUserView(/*GameUserVM vm*/)
        {
            if (vm.PrincipalPlanet == null || vm.Universe == null)
            {
                InitFakeDatas(vm);
            }

            return View(vm);
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
            if (buildingId != null)
            {
                using (var db = new ASPNetOgameLikeTPContext())
                {
                    var building = db.Buildings.Find(buildingId);
                    building.Level += 1;
                    db.Entry(building).State = EntityState.Modified;
                    db.SaveChanges();
                    vm.Universe.SolarSystems.SelectMany(x => x.Planets).SelectMany(x => x.Buildings).FirstOrDefault(x => x.Id == buildingId).Level++;
                }
            }

            return RedirectToAction("GameUserView");
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
            return PartialView("~/Views/GameUser/GameUserResources.cshtml", resources);
        }

        [ChildActionOnly]
        public ActionResult GetBuilding(Building building)
        {
            return PartialView("~/Views/GameUser/GameUserBuilding.cshtml", building);
        }
    }
}
