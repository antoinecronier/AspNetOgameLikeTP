using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASPNetOgameLikeTP.Data
{
    public class ASPNetOgameLikeTPContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ASPNetOgameLikeTPContext() : base("name=ASPNetOgameLikeTPContext")
        {
        }

        public System.Data.Entity.DbSet<ASPNetOgameLikeTPClassLibrary.Entities.Universe> Universes { get; set; }

        public System.Data.Entity.DbSet<ASPNetOgameLikeTPClassLibrary.Entities.SolarSystem> SolarSystems { get; set; }

        public System.Data.Entity.DbSet<ASPNetOgameLikeTPClassLibrary.Entities.Planet> Planets { get; set; }

        public System.Data.Entity.DbSet<ASPNetOgameLikeTPClassLibrary.Entities.Resource> Resources { get; set; }

        public System.Data.Entity.DbSet<ASPNetOgameLikeTPClassLibrary.Entities.Building> Buildings { get; set; }

        public System.Data.Entity.DbSet<ASPNetOgameLikeTPClassLibrary.Entities.Configuration> Configurations { get; set; }

        public void ClearDatabase()
        {
            using (var db = new ASPNetOgameLikeTPContext())
            {
                db.Buildings.RemoveRange(db.Buildings);
                //db.Configurations.RemoveRange(db.Configurations);
                db.Planets.RemoveRange(db.Planets);
                db.Resources.RemoveRange(db.Resources);
                db.SolarSystems.RemoveRange(db.SolarSystems);
                db.Universes.RemoveRange(db.Universes);

                db.SaveChanges();
            }
        }
    }
}
