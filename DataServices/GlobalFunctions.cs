using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace DataServices
{
    public class GlobalFunctions
    {

        public const string AdmProfile = "ADMIN";
        public const string WdProfile = "WORADMIN";
        public const string GAProfile = "GAPADMIN";
        public const string JsProfile = "JOSADMIN";
        public const string LpaBProfile = "LBELADMIN";
        public const string LpaVProfile = "LVLSADMIN";
        public const string RhProfile = "RHADMIN";
        public static List<string> GetAllProfiles()
        {
            return new List<string>
        {
            AdmProfile,
            WdProfile,
            GAProfile,
            JsProfile,
            LpaBProfile,
            LpaVProfile,
            RhProfile
        };

        }
        public static int SiteId
        {
            get
            {
                var defaultSiteId = int.Parse(WebConfigurationManager.AppSettings["DefaultSite"]);
                if (HttpContext.Current.User == null) return defaultSiteId;
                if (!HttpContext.Current.User.Identity.IsAuthenticated) return defaultSiteId;
                if (!(HttpContext.Current.User.Identity is FormsIdentity)) return defaultSiteId;

                return int.Parse(UserAuthService.RetornaUsuarioLogado().SiteId);
            }
        }

        /// <summary>
        /// Retorna o IPv4 do cliente conectado
        /// </summary>
        /// <returns></returns>
        public static string GetIpAddress(bool ipv6 = false)
        {
            var ipAddress = string.Empty;
            try
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                foreach (var ipa in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
                {
                    if ((!ipv6 && ipa.AddressFamily == AddressFamily.InterNetwork) || (ipv6 && ipa.AddressFamily == AddressFamily.InterNetworkV6))
                    {
                        ipAddress += (string.IsNullOrEmpty(ipAddress) ? "" : " / ");
                        ipAddress += ipa.ToString();
                    }
                }

                if (ipAddress != String.Empty)
                {
                    return ipAddress;
                }

                foreach (var ipa in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    if ((!ipv6 && ipa.AddressFamily == AddressFamily.InterNetwork) || (ipv6 && ipa.AddressFamily == AddressFamily.InterNetworkV6))
                    {
                        ipAddress += (string.IsNullOrEmpty(ipAddress) ? "" : " / ");
                        ipAddress += ipa.ToString();
                    }
                }
            }
            catch (Exception)
            {

            }


            return ipAddress;
        }

        /// <summary>
        /// Valida um endereço de e-mail
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string emailAddress)
        {
            const string validEmailPattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
            var regex = new Regex(validEmailPattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(emailAddress);
        }

        public static int WeekOfYearISO8601(DateTime date)
        {
            var day = (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.AddDays(4 - (day == 0 ? 7 : day)), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// Remove toda a marcação html
        /// </summary>
        /// <param name="inputHtml"></param>
        /// <returns></returns>
        public static string RemoveHtmlTags(string inputHTML)
        {
            //var noHtml = Regex.Replace(inputHTML, @"<[^>]+>|&nbsp;", "").Trim();
            //return Regex.Replace(noHtml, @"\s{2,}", " ");


            try
            {
                string result;

                // Remove HTML Development formatting
                // Replace line breaks with space
                // because browsers inserts space
                result = inputHTML.Replace("\r", " ");
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating spaces because browsers ignore them
                result = Regex.Replace(result,
                        @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = Regex.Replace(result,
                         @"<( )*head([^>])*>", "<head>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<( )*(/)( )*head( )*>)", "</head>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(<head>).*(</head>)", string.Empty,
                         RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = Regex.Replace(result,
                         @"<( )*script([^>])*>", "<script>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         RegexOptions.IgnoreCase);
                //result = Regex.Replace(result,
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty,
                //         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<script>).*(</script>)", string.Empty,
                         RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = Regex.Replace(result,
                         @"<( )*style([^>])*>", "<style>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<( )*(/)( )*style( )*>)", "</style>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(<style>).*(</style>)", string.Empty,
                         RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                result = Regex.Replace(result,
                         @"<( )*td([^>])*>", "\t",
                         RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = Regex.Replace(result,
                         @"<( )*br( )*>", "\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"<( )*li( )*>", "\r",
                         RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                //result = Regex.Replace(result,
                //         @"<( )*div([^>])*>", "\r\r",
                //         RegexOptions.IgnoreCase);
                //result = Regex.Replace(result,
                //         @"<( )*tr([^>])*>", "\r\r",
                //         RegexOptions.IgnoreCase);
                //result = Regex.Replace(result,
                //         @"<( )*p([^>])*>", "\r\r",
                //         RegexOptions.IgnoreCase);

                result = Regex.Replace(result,
                         @"<( )*div([^>])*>", "\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"<( )*tr([^>])*>", "\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"<( )*p([^>])*>", "\r",
                         RegexOptions.IgnoreCase);


                // Remove remaining tags like <a>, links, images,
                // comments etc - anything that's enclosed inside < >
                result = Regex.Replace(result,
                         @"<[^>]*>", string.Empty,
                         RegexOptions.IgnoreCase);

                // replace special characters:
                result = Regex.Replace(result,
                         @" ", " ",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&bull;", "*",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&lsaquo;", "<",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&rsaquo;", ">",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&trade;", "(tm)",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&frasl;", "/",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&lt;", "<",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&gt;", ">",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&copy;", "(c)",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&reg;", "(r)",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&ldquo;", "\"",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&rdquo;", "\"",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&lsquo;", "'",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&rsquo;", "'",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&ndash;", "-",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&mdash;", "—",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&ordm;", "º",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&ordf;", "ª",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&sect;", "§",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&#34;", "\"",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&#39;", "'",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&agrave", "à",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&aacute;", "á",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&atilde;", "ã",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&acirc;", "â",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&auml;", "ä",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&eacute;", "é",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&ecirc;", "ê",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&iacute;", "í",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&oacute;", "ó",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&otilde;", "õ",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&oslash;", "ø",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&uacute;", "ú",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&uuml;", "ü",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&ccedil;", "ç",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&agrave", "à",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&Aacute;", "Á",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&Atilde;", "Ã",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&Acirc;", "Â",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&Auml;", "Ä",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&Eacute;", "É",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&Ecirc;", "Ê",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&Iacute;", "Í",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&Oacute;", "Ó",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&Otilde;", "Õ",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&Oslash;", "Ø",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&Uacute;", "Ú",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&Uuml;", "Ü",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&Ccedil;", "Ç",
                         RegexOptions.IgnoreCase);

                // Remove all others. More can be added, see
                // http://hotwired.lycos.com/webmonkey/reference/special_characters/
                result = Regex.Replace(result,
                         @"&(.{2,6});", string.Empty,
                         RegexOptions.IgnoreCase);

                // for testing
                //Regex.Replace(result,
                //       this.txtRegex.Text,string.Empty,
                //       RegexOptions.IgnoreCase);

                // make line breaking consistent
                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4.
                // Prepare first to remove any whitespaces in between
                // the escaped characters and remove redundant tabs in between line breaks
                result = Regex.Replace(result,
                         "(\r)( )+(\r)", "\r\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(\t)( )+(\t)", "\t\t",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(\t)( )+(\r)", "\t\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(\r)( )+(\t)", "\r\t",
                         RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = Regex.Replace(result,
                         "(\r)(\t)+(\r)", "\r\r",
                         RegexOptions.IgnoreCase);
                // Remove multiple tabs following a line break with just one tab
                result = Regex.Replace(result,
                         "(\r)(\t)+", "\r\t",
                         RegexOptions.IgnoreCase);
                // Initial replacement target string for line breaks
                string breaks = "\r\r\r";
                // Initial replacement target string for tabs
                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                // That's it.
                return result;
            }
            catch
            {
                return inputHTML;
            }
        }

        public static void SetMailCulture()
        {
            var culture = new CultureInfo(SystemConfigurationService.GetValue("MailCulture"));
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        public static void SetDashboardCulture()
        {
            var culture = new CultureInfo(SystemConfigurationService.GetValue("ApplicationCulture"));
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        public static string GetIpFromHostname(string host)
        {
            if (host.Equals("localhost", StringComparison.CurrentCultureIgnoreCase))
                return host;
            var ip = string.Empty;
            IPHostEntry hostEntry;

            hostEntry = Dns.GetHostEntry(host);
            if (hostEntry.AddressList.Any())
                for (int i = 0; i <= hostEntry.AddressList.Length - 1; i++)
                {
                    if (!hostEntry.AddressList[i].IsIPv6LinkLocal)
                    {
                        ip = hostEntry.AddressList[i].ToString();
                    }
                }


            return ip;
        }

        /// <summary>
        /// Verifica as alterações das propriedades de dois objetos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalObject"></param>
        /// <param name="changedObject"></param>
        /// <returns></returns>
        public static List<string> GenerateAuditLogMessages<T>(T originalObject, T changedObject)
        {
            var list = new List<string>();
            foreach (PropertyInfo property in originalObject.GetType().GetProperties())
            {

                if (!property.CanRead || property.DeclaringType.AssemblyQualifiedName.Contains("System.Data.Entity.DynamicProxies"))
                    continue;

                object originalValue = property.GetValue(originalObject, null);
                object newValue = property.GetValue(changedObject, null);

                if (!object.Equals(originalValue, newValue))
                {
                    string originalText = (originalValue != null) ?
                        originalValue.ToString() : "[NULL]";
                    string newText = (newValue != null) ?
                        newValue.ToString() : "[NULL]";

                    if (originalText != newText)
                    {
                        list.Add(string.Concat(property.Name,
                            " changed from '", originalText,
                            "' to '", newText, "'"));
                    }
                }
            }

            return list;
        }

        public static string ReplaceHost(string original, string newHostName)
        {
            var builder = new UriBuilder(original);
            builder.Host = newHostName;
            return builder.Uri.ToString();
        }

        public static string GetHostFromUrl(string url)
        {
            Uri myUri = new Uri(url);
            return myUri.Host;
        }
        
        public static string UrlFromNameToIp(string url)
        {
            
            //Change to avoid replacing URL names by IP addresses

            //return url;

            try
            {
                var host = GetHostFromUrl(url);
                if (string.IsNullOrEmpty(host))
                    return url;

                var hostIp = GetIpFromHostname(host);
                if (string.IsNullOrEmpty(hostIp))
                    return url;

                return ReplaceHost(url, hostIp);
            }
            catch (Exception)
            {
                return url;
            }

        }

        public static bool ValidateCredentials(string username, string password)
        {
            using (System.DirectoryServices.AccountManagement.PrincipalContext context = new System.DirectoryServices.AccountManagement.PrincipalContext(ContextType.Domain))
            {
                return context.ValidateCredentials(username, password, ContextOptions.Negotiate);
            }
        }
    }
}
