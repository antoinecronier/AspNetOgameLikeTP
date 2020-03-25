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

        public System.Data.Entity.DbSet<ASPNetOgameLikeTPClassLibrary.Entities.SolarSystem> SolarSystems { get; set; }

        public System.Data.Entity.DbSet<ASPNetOgameLikeTPClassLibrary.Entities.Planet> Planets { get; set; }

        public System.Data.Entity.DbSet<ASPNetOgameLikeTPClassLibrary.Entities.Resource> Resources { get; set; }
    }
}
