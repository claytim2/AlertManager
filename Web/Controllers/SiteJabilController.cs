using DataServices;
using Localization.Web;
using Model.AbsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Business;

namespace Web.Controllers
{
    [Authorize]
    public class SiteJabilController : Controller
    {
        public ActionResult Index()
        {
            var menuItem = MenuBuilderService.GetMenuByTitle(ResMenu.GeneralEntriesJmd_Sites);
            ViewBag.Icon = menuItem.Icon;
            ViewBag.Title = menuItem.Title;
            ViewBag.SubTitle = menuItem.Description;
            ViewBag.VideoTutorialUrl = menuItem.VideoTutorialUrl;

            return View();
        }

        /// <summary>
        /// Método de consulta chamado pela datatable no preenchimento da tabela por ajax
        /// </summary>
        /// <param name="parametros">Parâmetros oriundos do datatable</param>
        /// <returns></returns>
        public ActionResult ListTable(TableParameter parametros)
        {
            new SiteJabilService().List(ref parametros);

            return Json(new
            {
                parametros.sEcho,
                iTotalRecords = parametros.TotalReg,
                iTotalDisplayRecords = parametros.DisplayReg,
                orderClasses = true,
                aaData = parametros.FinalData
            }, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// Retorna os dados para montagem da combo, componente Select2
        /// </summary>
        /// <param name="q"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public JsonResult Select2Format(string q = "", int pagesize = 10, int page = 1)
        {
            var entity = new SiteJabilService().GetByFilter(x => string.IsNullOrEmpty(q) ||
                                x.Name.ToUpper().Contains(q.ToUpper()));
            var filteredEntity = entity
                .OrderBy(x => x.Description)
                .Skip(pagesize * (page - 1)).Take(pagesize);
            var entityCount = entity.Count();
            var result = new
            {
                Results = filteredEntity.Select(a => new { id = a.Id, text = a.Description }),
                Total = entityCount
            };


            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}