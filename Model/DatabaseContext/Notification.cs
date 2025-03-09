using Localization.DataServices;
using Localization.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.DatabaseContext
{
   public class Notification : Common.BaseModel
    {
        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Notification_Area")]
        [Required(ErrorMessageResourceType = typeof(ResErrors), ErrorMessageResourceName = "FieldMandatory")]
        [StringLength(50)]
        public string Area { get; set; }

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Notification_Message")]
        [Required(ErrorMessageResourceType = typeof(ResErrors), ErrorMessageResourceName = "FieldMandatory")]
        [StringLength(300)]
        public string Message { get; set; }

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Notification_Active")]
        public bool Active { get; set; }

        public Notification()
        {
            Active = true;
        }
    }
}
