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
        // GET: Game/Create
        public ActionResult GameUserView(GameUserVM vm)
        {
            if (vm.PrincipalPlanet == null || vm.SolarSystem == null)
            {
                vm = new GameUserVM();
                InitFakeDatas(vm);
            }


            return View(vm);
        }

        private static void InitFakeDatas(GameUserVM vm)
        {
            SolarSystem ss = new SolarSystem();
            ss.Name = "system solaire 1";
            for (int i = 1; i < 10; i++)
            {
                ss.Planets.Add(new PlanetBuilder().Name("planet " + i).Build());
            }
            vm.PrincipalPlanet = new PlanetBuilder().Name("Principale").Build();
            ss.Planets.Add(vm.PrincipalPlanet);

            vm.SolarSystem = ss;

            try
            {
                using (var db = new ASPNetOgameLikeTPContext())
                {
                    db.SolarSystems.Add(ss);
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                Console.WriteLine("error : " + error.PropertyName);
                Console.WriteLine("message : " + error.ErrorMessage);
            }
        }

        public ActionResult UpgradeBuilding(ResourceGenerator building)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ASPNetOgameLikeTPContext())
                {
                    db.Buildings.Attach(building);
                    building.Level += 1;
                    db.Entry(building).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return View();
            //return PartialView("~/Views/GameUser/GameUserTopHeadband.cshtml", vm);
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
