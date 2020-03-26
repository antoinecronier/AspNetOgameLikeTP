using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNetOgameLikeTP.Utils
{
    public class ConfigurationsUtil
    {
        private static ConfigurationsUtil _instance;
        static readonly object instanceLock = new object();

        private ConfigurationsUtil()
        {
            JsonConvert.DeserializeObject(FileUtil.ReadServerFile("~/Content/Configurations/GameConfigurations.txt"));
        }

        public static ConfigurationsUtil Instance
        {
            get
            {
                if (_instance == null) //Les locks prennent du temps, il est préférable de vérifier d'abord la nullité de l'instance.
                {
                    lock (instanceLock)
                    {
                        if (_instance == null) //on vérifie encore, au cas où l'instance aurait été créée entretemps.
                            _instance = new ConfigurationsUtil();
                    }
                }
                return _instance;
            }
        }
    }
}