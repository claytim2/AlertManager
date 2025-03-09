using DataServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
#pragma warning disable CS1591
namespace Web.Business
{
    public class GlobalViewBags : ActionFilterAttribute
    {

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (HttpContext.Current.User == null) return;
            if (!HttpContext.Current.User.Identity.IsAuthenticated) return;
            if (!(HttpContext.Current.User.Identity is FormsIdentity)) return;

            filterContext.Controller.ViewBag.IsAdmin = UserAuthService.RetornaUsuarioLogado().IsAdmin;
        }
    }
}