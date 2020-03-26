using ASPNetOgameLikeTP.Builders;
using ASPNetOgameLikeTP.Models;
using ASPNetOgameLikeTPClassLibrary.Entities;
using System;
using System.Collections.Generic;
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
                SolarSystem ss = new SolarSystem();
                ss.Name = "ss1";
                for (int i = 1; i < 10; i++)
                {
                    ss.Planets.Add(new PlanetBuilder().Name("p"+i).Build());
                }
                vm.PrincipalPlanet = new PlanetBuilder().Name("Principale").Build();
                ss.Planets.Add(vm.PrincipalPlanet);

                vm.SolarSystem = ss;
            }
            

            return View(vm);
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
    }
}
