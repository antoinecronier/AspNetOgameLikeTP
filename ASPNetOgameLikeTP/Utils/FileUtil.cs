using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace ASPNetOgameLikeTP.Utils
{
    public static class FileUtil
    {
        public static String ReadServerFile(String path)
        {
            String result = null;
            using (StreamReader sr = new StreamReader(HostingEnvironment.MapPath(path)))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }
    }
}