using Infra;
using Model.AbsModels;
using System.Configuration;
using System;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Net.Http;
using System.Collections.Generic;

namespace DataServices
{
    public class UserAuthService
    {
        /// <summary>
        /// Retorna os dados do usuário logado
        /// </summary>
        /// <returns>Objeto da classe Usuario</returns>
        public static UserAuth RetornaUsuarioLogado()
        {
            try
            {
                if (HttpContext.Current.User == null || !HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return new UserAuth();
                }
                var id = (FormsIdentity)HttpContext.Current.User.Identity;
                var ticket = id.Ticket;

                var dados = ticket.UserData.Split(';');

                return new UserAuth
                {
                    UserName = id.Name,
                    Name = dados[0],
                    Email = dados[1],
                    Roles = dados[2],
                    LanguageId = dados[3],
                    Id = (dados.Length > 4 ? dados[4] : ""),
                    Registration = (dados.Length > 5 ? dados[5] : ""),
                    SiteId = (dados.Length > 6 ? dados[6] : ""),
                    SiteName = (dados.Length > 7 ? dados[7] : ""),
                    IsAdmin = GlobalFunctions.GetAllProfiles().Contains(dados[2]),
                    Avatar = (dados.Length > 8 ? dados[8] : "")
                };
            }
            catch (System.Exception)
            {
                return new UserAuth();
            }

        }

        /// <summary>
        /// Retorna o endereço do avatar do usuário logado
        /// </summary>
        /// <returns></returns>
        public static string GetAvatarImage(string registration)
        {
            var baseAddress = WebConfigurationManager.AppSettings["AvatarImagesBaseAddress"];
            var url = baseAddress + "/" + registration + ".jpg";

            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Timeout = 1200; // miliseconds
            webRequest.Method = "HEAD";

            try
            {
                webRequest.GetResponse();
            }
            catch
            {
                url = baseAddress + "/Default.jpg";
            }

            return url;
        }
    }
}
