using ASPNetOgameLikeTPClassLibrary.Entities;
using ASPNetOgameLikeTPClassLibrary.Entities.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ASPNetOgameLikeTP.Models
{
    public class GameAdminVM
    {
        public GlobalGameConfiguration GlobalGameConfiguration { get; set; }

        public GlobalPlanetConfiguration GlobalPlanetConfiguration { get; set; }

        public List<Resource> Resources { get; set; } = new List<Resource>();

        public List<Building> Buildings { get; set; } = new List<Building>();
    }
}