using ASPNetOgameLikeTPClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNetOgameLikeTP.Models
{
    public class GameUserVM
    {
        public Planet PrincipalPlanet { get; set; }

        public SolarSystem SolarSystem { get; set; }
    }
}