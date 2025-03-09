using Localization.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AbsModels
{
    public class MessageModel
    {
        public enum EMessageType
        {
            [Display(ResourceType = typeof(ResLabels), Description = "MessageModel_Success")]
            Success = 0,

            [Display(ResourceType = typeof(ResLabels), Description = "MessageModel_Information")]
            Information = 1,

            [Display(ResourceType = typeof(ResLabels), Description = "MessageModel_Alert")]
            Alert = 2,

            [Display(ResourceType = typeof(ResLabels), Description = "MessageModel_Error")]
            Error = 3
        }

        public string Text { get; set; }
        public string Title { get; set; }
        public EMessageType Type { get; set; }
    }
}
