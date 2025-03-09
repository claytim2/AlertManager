using DataServices;
using Infra;
using Localization.Web;
using Model.AbsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Business
{
    public class MenuBuilderService
    {
        /// <summary>
        /// Preenche a lista de objetos de menu
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Menu> Fill()
        {
            var admProfile = GlobalFunctions.AdmProfile;
            var wdProfile = GlobalFunctions.WdProfile;
            var GAProfile = GlobalFunctions.GAProfile;
            var jsProfile = GlobalFunctions.JsProfile;
            var lpaBProfile = GlobalFunctions.LpaBProfile;
            var lpaVProfile = GlobalFunctions.LpaVProfile;
            var rhProfile = GlobalFunctions.RhProfile;
            var menuItens = new List<Menu>
            {
                #region General Entries 1 - 1
                new Menu {
                    Id = 1,
                    Order = 1,
                    Title = ResMenu.Workday,
                    Description = "",
                    Icon = "fa-dashboard",
                    Controller = "",
                    Action = "Index",
                    VideoTutorialUrl = "",
                    Profiles = new string[]{ admProfile, wdProfile }
                },
                new Menu {
                    Id = 2,
                    Order = 2,
                    Title = ResMenu.GapAnalysis,
                    Description = "",
                    Icon = "fa-dashboard",
                    Controller = "",
                    Action = "",
                    VideoTutorialUrl = "",
                    Profiles = new string[]{ admProfile , GAProfile }
                },
                new Menu {
                    Id = 3,
                    Order = 3,
                    Title = ResMenu.Jos,
                    Description = "",
                    Icon = "fa-dashboard",
                    Controller = "",
                    Action = "",
                    VideoTutorialUrl = "",
                    Profiles = new string[]{ admProfile , jsProfile }
                },
                new Menu {
                    Id = 4,
                    Order = 4,
                    Title = ResMenu.LpaBel,
                    Description = "",
                    Icon = "fa-dashboard",
                    Controller = "",
                    Action = "",
                    VideoTutorialUrl = "",
                    Profiles = new string[]{ admProfile ,lpaBProfile}
                },
                new Menu {
                    Id = 5,
                    Order = 5,
                    Title = ResMenu.LpaVls,
                    Description = "",
                    Icon = "fa-dashboard",
                    Controller = "",
                    Action = "",
                    VideoTutorialUrl = "",
                    Profiles = new string[]{admProfile, lpaVProfile }
                },
                new Menu {
                    Id = 6,
                    Order = 6,
                    Title = ResMenu.Rh,
                    Description = "",
                    Icon = "fa-dashboard",
                    Controller = "Rh",
                    Action = "Index",
                    VideoTutorialUrl = "",
                    Profiles = new string[]{admProfile, rhProfile }
                },
                #endregion
                //------------------------------------------------------------------------------------------------------------------------------------
                #region General Entries 20 - 2
                new Menu {
                    Id = 21,
                    Order = 1,
                    ParentId = 1,
                    Title = ResMenu.Users,
                    Description = "",
                    Icon = "fa-users",
                    Controller = "Workday",
                    Action = "Index",
                    Profiles = new string[] { admProfile, wdProfile }
                },
                new Menu {
                    Id = 22,
                    Order = 2,
                    ParentId = 1,
                    Title = ResMenu.Notification,
                    Description = "",
                    Icon = "fa-commenting-o",
                    Controller = "Notification",
                    Action = "Index",
                    Profiles = new string[] { admProfile, wdProfile }
                },
                new Menu {
                    Id = 23,
                    Order = 3,
                    ParentId = 2,
                    Title = ResMenu.Users,
                    Description = "",
                    Icon = "fa-users",
                    Controller = "GapAnalysis",
                    Action = "Index",
                    Profiles = new string[] { admProfile, GAProfile }
                },
                new Menu {
                    Id = 24,
                    Order = 4,
                    ParentId = 2,
                    Title = ResMenu.Notification,
                    Description = "",
                    Icon = "fa-commenting-o",
                    Controller = "Notification",
                    Action = "Index",
                    Profiles = new string[] { admProfile, GAProfile }
                },
                new Menu {
                    Id = 25,
                    Order = 5,
                    ParentId = 3,
                    Title = ResMenu.Users,
                    Description = "",
                    Icon = "fa-users",
                    Controller = "Jos",
                    Action = "Index",
                    Profiles = new string[] { admProfile, jsProfile }
                },
                new Menu {
                    Id = 26,
                    Order = 6,
                    ParentId = 3,
                    Title = ResMenu.Notification,
                    Description = "",
                    Icon = "fa-commenting-o",
                    Controller = "Notification",
                    Action = "Index",
                    Profiles = new string[] { admProfile, jsProfile }
                },
                new Menu {
                    Id = 27,
                    Order = 7,
                    ParentId = 4,
                    Title = ResMenu.Users,
                    Description = "",
                    Icon = "fa-users",
                    Controller = "LpaBel",
                    Action = "Index",
                    Profiles = new string[] { admProfile, lpaBProfile }
                },
                new Menu {
                    Id = 28,
                    Order = 8,
                    ParentId = 4,
                    Title = ResMenu.Notification,
                    Description = "",
                    Icon = "fa-commenting-o",
                    Controller = "Notification",
                    Action = "Index",
                    Profiles = new string[] { admProfile, lpaBProfile }
                },
                new Menu {
                    Id = 29,
                    Order = 9,
                    ParentId = 5,
                    Title = ResMenu.Users,
                    Description = "",
                    Icon = "fa-users",
                    Controller = "LpaVls",
                    Action = "Index",
                    Profiles = new string[] { admProfile, lpaVProfile }
                },
                new Menu {
                    Id = 30,
                    Order = 10,
                    ParentId = 5,
                    Title = ResMenu.Notification,
                    Description = "",
                    Icon = "fa-commenting-o",
                    Controller = "Notification",
                    Action = "Index",
                    Profiles = new string[] { admProfile, lpaVProfile }
                },
                new Menu {
                    Id = 31,
                    Order = 11,
                    ParentId = 6,
                    Title = ResMenu.Users,
                    Description = "",
                    Icon = "fa-users",
                    Controller = "Rh",
                    Action = "Index",
                    Profiles = new string[] { admProfile, rhProfile }
                },
                new Menu {
                    Id = 32,
                    Order = 12,
                    ParentId = 6,
                    Title = ResMenu.Notification,
                    Description = "",
                    Icon = "fa-commenting-o",
                    Controller = "Notification",
                    Action = "Index",
                    Profiles = new string[] { admProfile, rhProfile }
                },
                //------------------------------------------------------------------------------------------------------------------------------------

                #endregion

                
                new Menu {
                    Id = 60,
                    Order = 6,
                    Title = ResMenu.Configuration,
                    Description = "",
                    Icon = "fa-gears",
                    Controller = "",
                    Action = "Index",
                    VideoTutorialUrl = "",
                    Profiles = new string[] { admProfile }
                },
                #region General Entries 2 - 2
                   new Menu {
                    Id = 70,
                    Order = 1,
                    ParentId = 60,
                    Title = ResMenu.Configuration,
                    Description = "",
                    Icon = "fa-gears",
                    Controller = "SystemConfiguration",
                    Action = "Index",
                    Profiles = new string[] { admProfile }
                },
                    new Menu {
                    Id = 72,
                    Order = 2,
                    ParentId = 60,
                    Title = ResMenu.Location,
                    Description = "",
                    Icon = "fa-home",
                    Controller = "Location",
                    Action = "Index",
                    Profiles = new string[] { admProfile }
                },
                #endregion
                new Menu {
                    Id = 90,
                    Order = 7,
                    Title = ResMenu.UserSys,
                    Description = "",
                    Icon = "fa-users",
                    Controller = "Users",
                    Action = "Index",
                    VideoTutorialUrl = "",
                    Profiles = new[] {admProfile}
                },

                #region "Log 80 - 8"
                new Menu {
                    Id = 80,
                    Order = 8,
                    Title = ResMenu.Log,
                    Description = "",
                    Icon = "fa-file-text-o",
                    Controller = "",
                    Action = "",
                    Profiles = new[] {admProfile}
                },
                new Menu {
                    Id = 81,
                    ParentId = 80,
                    Order = 1,
                    Title = ResMenu.Log_Login,
                    Description = "",
                    Icon = "fa-user",
                    Controller = "Logs",
                    Action = "Index",
                    Parameters = "Type=" + EnumLists.ELogType.Login,
                    VideoTutorialUrl = "",
                    Profiles = new[] {admProfile}
                },
                new Menu {
                    Id = 82,
                    ParentId = 80,
                    Order = 2,
                    Title = ResMenu.Log_AddRecord,
                    Description = "",
                    Icon = "fa-plus-circle",
                    Controller = "Logs",
                    Action = "Index",
                    Parameters = "Type=" + EnumLists.ELogType.AddRecord,
                    VideoTutorialUrl = "",
                    Profiles = new[] {admProfile}
                },
                new Menu {
                    Id = 83,
                    ParentId = 80,
                    Order = 3,
                    Title = ResMenu.Log_UpdateRecord,
                    Description = "",
                    Icon = "fa-arrow-circle-down",
                    Controller = "Logs",
                    Action = "Index",
                    Parameters = "Type=" + EnumLists.ELogType.UpdateRecord,
                    VideoTutorialUrl = "",
                    Profiles = new[] {admProfile}
                },
                new Menu {
                    Id = 84,
                    ParentId = 80,
                    Order = 4,
                    Title = ResMenu.Log_DeleteRecord,
                    Description = "",
                    Icon = "fa-times-circle",
                    Controller = "Logs",
                    Action = "Index",
                    Parameters = "Type=" + EnumLists.ELogType.DeleteRecord,
                    VideoTutorialUrl = "",
                    Profiles = new[] {admProfile}
                },
                new Menu {
                    Id = 85,
                    ParentId = 80,
                    Order = 5,
                    Title = ResMenu.Log_Email,
                    Description = "",
                    Icon = "fa-envelope",
                    Controller = "Logs",
                    Action = "Index",
                    Parameters = "Type=" + EnumLists.ELogType.Email,
                    VideoTutorialUrl = "",
                    Profiles = new[] {admProfile}
                },
                new Menu {
                    Id = 86,
                    ParentId = 80,
                    Order = 6,
                    Title = ResMenu.Log_Errors,
                    Description = "",
                    Icon = "fa-envelope",
                    Controller = "Logs",
                    Action = "Index",
                    Parameters = "Type=" + EnumLists.ELogType.Error,
                    VideoTutorialUrl = "",
                    Profiles = new[] {admProfile}
                },
                  new Menu {
                    Id = 87,
                    ParentId = 80,
                    Order = 5,
                    Title = ResMenu.Log_Service,
                    Description = "",
                    Icon = "fa-cogs",
                    Controller = "Logs",
                    Action = "Index",
                    Parameters = "Type=" + EnumLists.ELogType.Service,
                    VideoTutorialUrl = "",
                    Profiles = new[] {admProfile}
                },
                #endregion

            };

            return menuItens;
        }

        /// <summary>
        /// Retorna um item do menu pelo título informado
        /// </summary>
        /// <param name="title">Título do Item do menu</param>
        /// <returns></returns>
        public static Menu GetMenuByTitle(string title)
        {
            return Fill().FirstOrDefault(t => t.Title.Equals(title));
        }

        /// <summary>
        /// Retorna um item do menu pelo ID informado
        /// </summary>
        /// <param name="id">Id do Item do Menu</param>
        /// <returns></returns>
        public static Menu GetMenuById(int id)
        {
            return Fill().FirstOrDefault(t => t.Id.Equals(id));
        }

        /// <summary>
        /// Controla se o menu ou algum submenu está selecionado
        /// </summary>
        static bool _hasSelected;

        /// <summary>
        /// Cria o código HTML do menu do usuário
        /// </summary>
        /// <returns></returns>
        public static string MenuMaker()
        {

            var menuString = "";
            var subMenuString = "";
            var controller = HttpContext.Current.Request.RequestContext.RouteData.GetRequiredString("controller");
            var action = HttpContext.Current.Request.RequestContext.RouteData.GetRequiredString("action");
            var userProfile = UserAuthService.RetornaUsuarioLogado().Roles;
            var menuItens = Fill().Where(t => t.ParentId.Equals(0)).Where(t => !t.Profiles.Any() || t.Profiles.Contains(userProfile)).OrderBy(t => t.Order);

            foreach (var item in menuItens)
            {
                _hasSelected = false;
                var u = new UrlHelper(HttpContext.Current.Request.RequestContext);
                var url = string.IsNullOrEmpty(item.Controller) ? "#" : u.Action(item.Action, item.Controller, null);
                url += (!string.IsNullOrEmpty(item.Parameters) ? "?" + item.Parameters : "");
                var menuSubItens = Fill().Where(t => t.ParentId.Equals(item.Id)).Where(t => !t.Profiles.Any() || t.Profiles.Contains(userProfile)).OrderBy(t => t.Order);
                var hasSub = menuSubItens.Any();

                if (hasSub)
                {
                    var fakeParam = true;
                    subMenuString = SubMenuMaker(item.Id, ref fakeParam);
                }

                menuString += "<li class='" + (hasSub ? " treeview " : "") + ((controller.Equals(item.Controller, System.StringComparison.CurrentCultureIgnoreCase) && action.Equals(item.Action, System.StringComparison.CurrentCultureIgnoreCase)) || _hasSelected ? " active " : "") + "'>" +
                    "<a href = '" + url + "' target='" + (item.NewWindow ? "_blank" : "_self") + "'>" +
                    "<i class='fa " + item.Icon + "'></i> <span>" + item.Title + "</span>" +
                    (hasSub ? "<i class='fa fa-angle-left pull-right'></i>" : "") +
                    "</a>";

                if (hasSub)
                {
                    menuString += "<ul class='treeview-menu'>";
                    menuString += subMenuString;
                    menuString += "</ul>";
                }
                menuString += "</li>";
            }


            return menuString;
        }

        /// <summary>
        /// Cria o código HTML dos sub menus
        /// </summary>
        /// <param name="idParent">Id do item do menu superior</param>
        /// <returns></returns>
        private static string SubMenuMaker(int idParent, ref bool selected)
        {
            var menuString = "";
            var controller = HttpContext.Current.Request.RequestContext.RouteData.GetRequiredString("controller");
            var action = HttpContext.Current.Request.RequestContext.RouteData.GetRequiredString("action");
            var userProfile = UserAuthService.RetornaUsuarioLogado().Roles;
            var menuItens = Fill().Where(t => t.ParentId.Equals(idParent)).Where(t => !t.Profiles.Any() || t.Profiles.Contains(userProfile)).OrderBy(t => t.Order);

            foreach (var item in menuItens)
            {
                var selectedLocal = false;
                var menuStringLocal = "";
                var u = new UrlHelper(HttpContext.Current.Request.RequestContext);
                var url = u.Action(item.Action, item.Controller, null);
                url += (!string.IsNullOrEmpty(item.Parameters) ? "?" + item.Parameters : "");

                var menuSubItens = Fill().Where(t => t.ParentId.Equals(item.Id)).Where(t => !t.Profiles.Any() || t.Profiles.Contains(userProfile)).OrderBy(t => t.Order);
                var hasSub = menuSubItens.Any();

                var isSelected = controller.Equals(item.Controller, StringComparison.CurrentCultureIgnoreCase) && action.Equals(item.Action, StringComparison.CurrentCultureIgnoreCase);
                if (!selected)
                    selected = isSelected;

                if (!hasSub && !_hasSelected)
                    _hasSelected = controller.Equals(item.Controller, System.StringComparison.CurrentCultureIgnoreCase);

                if (hasSub)
                {
                    menuStringLocal += "<ul class='treeview-menu'>";

                    menuStringLocal += SubMenuMaker(item.Id, ref selectedLocal);

                    menuStringLocal += "</ul>";
                }

                if (!isSelected)
                    isSelected = selectedLocal;

                menuString += "<li class='" + (hasSub ? " treeview " : "") + (isSelected ? " active " : "") + "'>" +
                    "<a href = '" + url + "' target='" + (item.NewWindow ? "_blank" : "_self") + "'>" +
                    "<i class='fa " + item.Icon + "'></i> <span>" + item.Title + "</span>" +
                    (hasSub ? "<i class='fa fa-angle-left pull-right'></i>" : "") +
                    "</a>";
                menuString += menuStringLocal;
                menuString += "</li>";

            }


            return menuString;
        }
    }
}
