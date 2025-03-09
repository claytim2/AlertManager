using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class VmDashboard
    {
        public string DateRange { get; set; }

        public DateTime StartDate => DateTime.Parse(DateRange.Split('-')[0].Trim());

        public DateTime EndDate => DateTime.Parse(DateRange.Split('-')[1].Trim());

        public string PeriodType { get; set; }
    }
}