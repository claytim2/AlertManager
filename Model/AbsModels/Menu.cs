using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AbsModels
{
    public class Menu
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int ParentId { get; set; }
        public string[] Profiles { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Parameters { get; set; }
        public bool NewWindow { get; set; }
        public string VideoTutorialUrl { get; set; }

        public Menu()
        {
            NewWindow = false;
        }
    }
}
