using Localization.Model;
using Model.DatabaseContext.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Infra.EnumLists;

namespace Model.DatabaseContext
{
    public class Log : BaseModel
    {
        [Index(Order = 1)]
        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Log_DateTime")]
        public DateTime DateTime { get; set; }

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Log_IpAddress")]
        public string IpAddress { get; set; }

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Log_LogType")]
        public ELogType LogType { get; set; }

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Log_Status")]
        public bool Status { get; set; }

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Log_Description")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(ResDatabaseContext), Name = "Log_UserName")]
        public string UserName { get; set; }


        public Log()
        {
            DateTime = DateTime.Now;
        }
    }
}
