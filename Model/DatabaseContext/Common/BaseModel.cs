using Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DatabaseContext.Common
{
    public class BaseModel : IBaseModel
    {
        [Key]
        public int Id { get; set; }

        public int SiteId { get; set; }

        public string UserCreate { get; set; }

        public DateTime? DateCreate { get; set; }

        public string UserUpdate { get; set; }

        public DateTime? DateUpdate { get; set; }

        public BaseModel()
        {
            DateCreate = DateTime.Now;
            SiteId = 1;
        }
    }
}
