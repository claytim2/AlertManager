
using Model.AbsModels;
using Model.ViewModel;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;

namespace DataServices
{
    public class AIOServiceWebApi
    {
        HttpClient _client;

        public AIOServiceWebApi()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["AIOServiceAddress"]),
            };
        }

        /// <summary>
        /// Api de autenticação no JMD
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public JmdTicket JmdGetAuthentication(VmLogin token)
        {
            try
            {
                var userService = new UserService();

                var userDB = userService.GetByFilter(p => p.UserName.ToUpper().Equals(token.UserName, StringComparison.InvariantCultureIgnoreCase) && p.Active).FirstOrDefault();

                var returnData = new JmdTicket();

                if (userDB == null)
                {
                    returnData.Message = "Username was not recognized";
                    return returnData;
                }
                else
                {
                    //Domain Authentication
                    var auth = GlobalFunctions.ValidateCredentials(token.UserName, token.Password);
                    if (!auth)
                    {
                        returnData.Message = "Invalid Domain Credentials";
                        return returnData;
                    }
                    else
                    {
                        returnData = new JmdTicket
                        {
                            Profile = userDB.Roles,
                            StatusRequest = true,
                            MesSiteId = 1,
                            MesSiteName = "Betim",
                            Token = "",

                            UserId = userDB.Id,
                            Email = userDB.Email,
                            JmdSiteId = 1,
                            JmdSiteName = "Betim",
                            LanguageId = 2,
                            Name = userDB.Name,
                            UserName = userDB.UserName,
                            Registration = userDB.Registration,
                            AvatarAddress = JmdGetUserAvatar(userDB.Registration),
                    };
                    }
                }

                return returnData;
            }
            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// Get User Avatar
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public string JmdGetUserAvatar(string registration)
        {
            var apiDataJmd = _client.GetAsync("Jmd/GetAvatarAddress/" + registration).Result;
            var returnData = "";

            if (apiDataJmd.IsSuccessStatusCode)
            {
                returnData = apiDataJmd.Content.ReadAsAsync<string>().Result;
            }

            return returnData;

        }



    }
}
