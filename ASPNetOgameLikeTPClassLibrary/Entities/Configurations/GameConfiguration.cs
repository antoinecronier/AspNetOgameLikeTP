using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Entities.Configurations
{
    public class GameConfiguration
    {
        public List<ResourceGenerator> ResourceGenerators { get; set; } = new List<ResourceGenerator>();

        public List<Resource> Resources { get; set; } = new List<Resource>();
    }
}
