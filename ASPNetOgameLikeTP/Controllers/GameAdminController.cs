using ASPNetOgameLikeTP.Data;
using ASPNetOgameLikeTP.Models;
using ASPNetOgameLikeTPClassLibrary.Entities;
using ASPNetOgameLikeTPClassLibrary.Extensions;
using System;
using System.Collections.Generic;
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
            var globalConf = db.Configurations.Find(ConfigurationKeys.GlobalGameConfiguration.GetName());
            var planetConf = db.Configurations.Find(ConfigurationKeys.GlobalPlanetConfiguration.GetName());

            return View(vm);
        }

        [HttpPost]
        public ActionResult Configure(FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
