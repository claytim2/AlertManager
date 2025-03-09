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
    public class Location : Common.BaseModel
    {
        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Location_Area")]
        [Required(ErrorMessageResourceType = typeof(ResErrors), ErrorMessageResourceName = "FieldMandatory")]
        [StringLength(50)]
        public string Area { get; set; }

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Location_Description")]
        [Required(ErrorMessageResourceType = typeof(ResErrors), ErrorMessageResourceName = "FieldMandatory")]
        [StringLength(50)]
        public string Description { get; set; }

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Location_Active")]
        public bool Active { get; set; }

        public Location()
        {
            Active = true;
        }
    }
}