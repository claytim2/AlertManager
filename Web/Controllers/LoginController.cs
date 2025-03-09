using DataServices;
using Infra;
using Localization.DataServices;
using Localization.Web;
using Model.ViewModel;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Web.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Logout", "Login");

            return View(new VmLogin());

        }

        /// <summary>
        /// Logout do sistema
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
                LogService.Save(EnumLists.ELogType.Login, ResLabels.ResourceManager.GetString("LogLogout", new CultureInfo(SystemConfigurationService.GetValue("LogCulture"))));

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Login");

        }

        /// <summary>
        /// Método que recebe o token do JMD
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(VmLogin token)
        {
            //Forçando o logout
            if (User != null && User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }

            if (token == null || string.IsNullOrWhiteSpace(token.UserName) || string.IsNullOrWhiteSpace(token.Password))
            {
                token = new VmLogin { ErrorMessage = "Invalid Credentials" };
                return View(token);
            }

            var user = new AuthenticationService().TokenValidate(token);

            if (!user.StatusRequest)
            {
                token.ErrorMessage = user.Message;
                return View(token);
            }

            //Processo de autenticação
            var ticket = new FormsAuthenticationTicket(
                    1, // Versão do ticket
                    user.UserName, // Nome de usuário a ser associado a esse ticket
                    DateTime.Now, // Data/hora de concessão
                    DateTime.Now.AddDays(1), // Data/hora de expiração
                    false, // Se o cookie é persistente

                    user.Name + ";" +
                    user.Email + ";" +
                    user.Profile + ";" +
                    user.LanguageId + ";" +
                    user.UserId + ";" +
                    user.Registration + ";" +
                    user.MesSiteId + ";" +
                    user.MesSiteName + ";" +
                    user.AvatarAddress,

                FormsAuthentication.FormsCookiePath); // Caminho de validade do ticket

            // Encriptando o ticket
            var hash = FormsAuthentication.Encrypt(ticket);

            // Criando o cookie
            var cookie = new HttpCookie(
            FormsAuthentication.FormsCookieName, // Nome do cookie de autenticação (especificado no web.config)
             hash); // Ticket criptografado

            // Se for persistente, seta a expiração do cookie igual a expiração do ticket
            if (ticket.IsPersistent)
            {
                //cookie.Expires = ticket.Expiration;
            }

            var language = (user.LanguageId == 1 ? "pt-br" : "en");
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            // Adiciona o cookie ao navegador
            Response.Cookies.Add(cookie);
            //LogApp.Save(Domain.AbsModels.EnumLists.ELogType.Login, ResLabels.ResourceManager.GetString("LogLogin", new CultureInfo(ConfigurationApp.GetValue("LogCulture"))), true, user.UserName, user.SiteId);
            LogService.Save(EnumLists.ELogType.Login, ResLabels.ResourceManager.GetString("LogLogin", new CultureInfo(SystemConfigurationService.GetValue("LogCulture"))), true, user.UserName, user.MesSiteId);
            return RedirectToAction("Index", "Home");

        }
    }
}