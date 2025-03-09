using Localization.DataServices;
using Localization.Model;
using Model.DatabaseContext.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Resources;
using static Infra.EnumLists;

namespace Model.DatabaseContext
{
    public class SystemConfiguration : BaseModel
    {

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Configuration_Key")]
        public string Key { get; set; }

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Configuration_Description")]
        [NotMapped]
        public string Description
        {
            get
            {
                //Internacionalizando
                var rm = new ResourceManager("Localization.DataServices.ResConfiguration", typeof(ResConfiguration).Assembly);
                return rm.GetString("CFG_" + Key);
            }
        }

        [NotMapped]
        public EConfigurationType ConfigurationType { get; set; }

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Configuration_Value")]
        [Required(ErrorMessageResourceType = typeof(ResErrors), ErrorMessageResourceName = "FieldMandatory")]
        public string Value { get; set; }


        /// <summary>
        /// Retorna os valores default das configurações
        /// </summary>
        /// <returns></returns>
        public List<SystemConfiguration> GetDefaultValues()
        {
            var configurations = new List<SystemConfiguration> {
                new SystemConfiguration { Key="ClearLogDays", ConfigurationType = EConfigurationType.Number, Value ="0" },
                new SystemConfiguration { Key="LogCulture", ConfigurationType = EConfigurationType.Language, Value ="pt-br" },
                new SystemConfiguration { Key="ApplicationCulture", ConfigurationType = EConfigurationType.Language, Value ="pt-br" },
                new SystemConfiguration { Key="ApplicationPath", ConfigurationType = EConfigurationType.Language, Value ="" },
            };

            return configurations;
        }
    }
}
