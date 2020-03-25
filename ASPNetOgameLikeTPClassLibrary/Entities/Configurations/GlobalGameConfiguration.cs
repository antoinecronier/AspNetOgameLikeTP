using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Entities.Configurations
{
    public class GlobalGameConfiguration
    {
        [DisplayName("Nombre de système solaire :")]
        public int? SolarSystemNb { get; set; }

        [DisplayName("Nombre de planète par système :")]
        public int? PlanetsNb { get; set; }
    }
}
