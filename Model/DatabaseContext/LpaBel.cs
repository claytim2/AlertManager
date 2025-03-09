using Localization.DataServices;
using Localization.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DatabaseContext
{
    public class LpaBel : Common.BaseModel
    {
        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Users_EmployeeID")]
        [Required(ErrorMessageResourceType = typeof(ResErrors), ErrorMessageResourceName = "FieldMandatory")]
        [StringLength(50)]
        public string EmployeeID { get; set; }

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Users_Registration")]
        [Required(ErrorMessageResourceType = typeof(ResErrors), ErrorMessageResourceName = "FieldMandatory")]
        [StringLength(50)]
        public string Registration { get; set; }

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



        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Users_Notification")]
        public bool Notification { get; set; }

        public LpaBel()
        {
            Notification = true;
        }
    }
}

