﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.JmdDataServices
{
    public class SiteJabil
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
