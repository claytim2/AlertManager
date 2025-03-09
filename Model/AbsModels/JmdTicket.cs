using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AbsModels
{
    public class JmdTicket
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public bool StatusRequest { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Profile { get; set; }
        public int JmdSiteId { get; set; }
        public string JmdSiteName { get; set; }
        public int MesSiteId { get; set; }
        public string MesSiteName { get; set; }
        public int LanguageId { get; set; }
        public string LogoutUrl { get; set; }
        public string AvatarAddress { get; set; }
        public string Registration { get; set; }
    }
}
