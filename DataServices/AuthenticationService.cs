using Model.AbsModels;
using Model.ViewModel;
using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace DataServices
{
    public class AuthenticationService
    {
        /// <summary>
        /// Retorna a url de logout do JMD
        /// </summary>
        /// <returns></returns>
        public string LogoutUrl()
        {
            var url = new UrlHelper(HttpContext.Current.Request.RequestContext).Action(
            "Index",
            "Login",
            null,
            HttpContext.Current.Request.Url != null ? HttpContext.Current.Request.Url.Scheme : "http" );
            return url;


        }

        /// <summary>
        /// Faz a verificação do token no JMD
        /// </summary>
        /// <returns></returns>
        public JmdTicket TokenValidate(VmLogin token)
        {
            return new AIOServiceWebApi().JmdGetAuthentication(token);
        }
    }
}
