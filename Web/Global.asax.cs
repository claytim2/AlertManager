using DataServices;
using System;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Web.Business;

namespace Web
{
    public class MvcApplication : HttpApplication
    {
        public object GlobalConfiguration { get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new GlobalViewBags(), 0);

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

            if (HttpContext.Current.User == null) return;
            if (!HttpContext.Current.User.Identity.IsAuthenticated) return;
            if (!(HttpContext.Current.User.Identity is FormsIdentity)) return;

            // Get Forms Identity From Current User
            var id = (FormsIdentity)HttpContext.Current.User.Identity;
            // Get Forms Ticket From Identity object
            var ticket = id.Ticket;
            // Retrieve stored user-data (our roles from db)
            var userData = ticket.UserData;
            var roles = userData.Split(';')[2].Split(',');
            // Create a new Generic Principal Instance and assign to Current User
            HttpContext.Current.User = new GenericPrincipal(id, roles);

            var user = UserAuthService.RetornaUsuarioLogado();
            if (!string.IsNullOrEmpty(user.LanguageId))
            {
                var culture = new CultureInfo(user.LanguageId == "1" ? "pt-br" : "en");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }
    }
}
