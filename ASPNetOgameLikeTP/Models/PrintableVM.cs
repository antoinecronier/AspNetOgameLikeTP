using ASPNetOgameLikeTPClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNetOgameLikeTP.Models
{
    public class PrintableVM
    {
        public List<Resource> Resources { get; set; }

        public List<ResourceGenerator> ResourceGenerators { get; set; }

        public List<String> Images { get; set; }

        public List<Dictionary<String,String>> ResourceNames { get; set; }

        public List<Dictionary<String, String>> ResourceGeneratorNames { get; set; }

    }
}