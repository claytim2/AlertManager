using Localization.Model;
using Model.DatabaseContext.Common;
using System.ComponentModel.DataAnnotations;

namespace Model.DatabaseContext
{
    public class AboutConfiguration : BaseModel
    {

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "AboutConfiguration_Contact1Name")]
        public string Contact1Name { get; set; }
        [Display(ResourceType = typeof(ResDatabaseContext), Name = "AboutConfiguration_Contact1Email")]
        public string Contact1Email { get; set; }
        [Display(ResourceType = typeof(ResDatabaseContext), Name = "AboutConfiguration_Contact1Phone")]
        public string Contact1Phone { get; set; }

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "AboutConfiguration_Contact2Name")]
        public string Contact2Name { get; set; }
        [Display(ResourceType = typeof(ResDatabaseContext), Name = "AboutConfiguration_Contact2Email")]
        public string Contact2Email { get; set; }
        [Display(ResourceType = typeof(ResDatabaseContext), Name = "AboutConfiguration_Contact2Phone")]
        public string Contact2Phone { get; set; }


        [Display(ResourceType = typeof(ResDatabaseContext), Name = "AboutConfiguration_Contact3Name")]
        public string Contact3Name { get; set; }
        [Display(ResourceType = typeof(ResDatabaseContext), Name = "AboutConfiguration_Contact3Email")]
        public string Contact3Email { get; set; }
        [Display(ResourceType = typeof(ResDatabaseContext), Name = "AboutConfiguration_Contact3Phone")]
        public string Contact3Phone { get; set; }


        [Display(ResourceType = typeof(ResDatabaseContext), Name = "AboutConfiguration_Contact4Name")]
        public string Contact4Name { get; set; }
        [Display(ResourceType = typeof(ResDatabaseContext), Name = "AboutConfiguration_Contact4Email")]
        public string Contact4Email { get; set; }
        [Display(ResourceType = typeof(ResDatabaseContext), Name = "AboutConfiguration_Contact4Phone")]
        public string Contact4Phone { get; set; }
    }
}
