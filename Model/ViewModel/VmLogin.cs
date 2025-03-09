using Localization.DataServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class VmLogin
    {
        [Required(ErrorMessageResourceType = typeof(ResErrors), ErrorMessageResourceName = "FieldMandatory")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResErrors), ErrorMessageResourceName = "FieldMandatory")]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

    }
}
