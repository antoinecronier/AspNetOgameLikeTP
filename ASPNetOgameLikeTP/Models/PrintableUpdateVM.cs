using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNetOgameLikeTP.Models
{
    public class PrintableUpdateVM
    {
        public long? Id { get; set; }
        public String Name { get; set; }
        public int Type { get; set; }
        public String Print { get; set; }
    }
}