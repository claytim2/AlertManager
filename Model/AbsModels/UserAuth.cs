using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS1591
namespace Model.AbsModels
{
    /// <summary>
    /// 
    /// </summary>
    public class UserAuth
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public string LanguageId { get; set; }
        public string Registration { get; set; }
        public string Id { get; set; }
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public bool IsAdmin { get; set; }
        public string Avatar { get; set; }
    }
}
