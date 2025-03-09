using Localization.DataServices;
using Localization.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model.DatabaseContext
{
#pragma warning disable CS1591
    public class Users : Common.BaseModel
    {
        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Users_Name")]
        [Required(ErrorMessageResourceType = typeof(ResErrors), ErrorMessageResourceName = "FieldMandatory")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Users_UserName")]
        [Required(ErrorMessageResourceType = typeof(ResErrors), ErrorMessageResourceName = "FieldMandatory")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Users_Email")]
        [Required(ErrorMessageResourceType = typeof(ResErrors), ErrorMessageResourceName = "FieldMandatory")]
        [StringLength(150)]
        public string Email { get; set; }

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Users_Roles")]
        [Required(ErrorMessageResourceType = typeof(ResErrors), ErrorMessageResourceName = "FieldMandatory")]
        [StringLength(50)]
        public string Roles { get; set; }

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Users_Registration")]
        [Required(ErrorMessageResourceType = typeof(ResErrors), ErrorMessageResourceName = "FieldMandatory")]
        [StringLength(50)]
        public string Registration { get; set; }

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Users_Active")]
        public bool Active { get; set; }

        public Users()
        {
            Active = true;
        }

    }
}
