using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Web.Business
{
    public static class WebFunctions
    {
        /// <summary>
        /// Gera o link de include do arquivo javascript com data e hora, para evitar cache
        /// </summary>
        /// <param name="javascriptFile"></param>
        /// <returns></returns>
        public static string JavascriptFileImport(string javascriptFile)
        {
            var result = "";
            var file = HttpContext.Current.Server.MapPath(javascriptFile);
            if (File.Exists(file))
                result = "<script src=\"" + GetVirtualPath(file) + "?v=" + File.GetLastWriteTime(file).ToString("yyyyMMddHHmmss") + "\"></script>";

            return result;
        }

        private static string GetVirtualPath(string physicalPath)
        {
            var appPath = 
                //HttpContext.Current.Request.Url.Scheme + "://" + 
                //HttpContext.Current.Request.Url.Authority +
                HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/" + 
                physicalPath.Replace(HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"], string.Empty);
            appPath = appPath.Replace(@"\", "/");
            return appPath;
        }
    }


}