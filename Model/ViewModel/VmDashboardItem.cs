using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class VmDashboardItem
    {
        public string Address { get; set; }
        public int DashboardId { get; set; }
        public string NextDashboardAddress { get; set; }
        public int SecondsDisplay { get; set; }
    }
}
