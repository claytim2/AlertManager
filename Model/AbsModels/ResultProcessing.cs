using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AbsModels
{
    public class ResultProcessing
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object AdditionalData { get; set; }


        public ResultProcessing()
        {
            Success = false;
            Message = "";
            AdditionalData = null;
        }

    }
}
