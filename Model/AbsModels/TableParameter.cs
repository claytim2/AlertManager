using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AbsModels
{
    public class TableParameter
    {
        public string sEcho { get; set; }
        public int iDisplayStart { get; set; }
        public int iDisplayLength { get; set; }
        public string sSearch { get; set; }
        public string iSortCol_0 { get; set; }
        public string sSortDir_0 { get; set; }

        public int TotalReg { get; set; }
        public int DisplayReg { get; set; }
        public IEnumerable FilteredData { get; set; }
        public IEnumerable FinalData { get; set; }
        public int  ActiveCount { get; set; }
    }
}
