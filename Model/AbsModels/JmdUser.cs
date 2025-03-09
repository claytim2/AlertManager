using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AbsModels
{
    public class JmdUser
    {
        public int idUser { get; set; }
        public string sMatricula { get; set; }
        public string sName { get; set; }
        public string sPassword { get; set; }
        public string sEmail { get; set; }
        public string sLogin { get; set; }
        public string sStatus { get; set; }
        public int iLanguage { get; set; }
        public int idOriginSite { get; set; }
        public string sModifier { get; set; }
        public string dLastUpDate { get; set; }
    }
}
