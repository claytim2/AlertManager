using DataServices;
using Infra;
using Localization.DataServices;
using Localization.Web;
using Model.AbsModels;
using Model.DatabaseContext;
using System.Web.Mvc;
using Web.Business;

namespace Web.Controllers
{
    [Authorize(Roles = GlobalFunctions.AdmProfile)]
    public class LogsController : Controller
    {
        public ActionResult Index(EnumLists.ELogType type)
        {
            var menuItem = MenuBuilderService.GetMenuByTitle(ResMenu.Log);
            ViewBag.Icon = menuItem.Icon;
            ViewBag.Title = menuItem.Title + " - " + EnumLists.GetLocalizedDisplay<EnumLists.ELogType>(type.ToString());
            ViewBag.SubTitle = menuItem.Description;
            ViewBag.VideoTutorialUrl = menuItem.VideoTutorialUrl;

            return View();
        }

        /// <summary>
        /// Método de consulta chamado pela datatable no preenchimento da tabela por ajax
        /// </summary>
        /// <param name="parametros">Parâmetros oriundos do datatable</param>
        /// <param name="logType"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ListTable(TableParameter parametros, EnumLists.ELogType logType)
        {
            //Necessário ajuste nos parâmetros quando há variáveis customizadas
            parametros.iDisplayLength = int.Parse(Request.Params["length"]);
            parametros.iDisplayStart = int.Parse(Request.Params["start"]);
            parametros.iSortCol_0 = Request.Params["order[0][column]"];
            parametros.sSearch = Request.Params["search[value]"];
            parametros.sSortDir_0 = Request.Params["order[0][dir]"];
            parametros.FinalData = new string[] { };

            new LogService().List(ref parametros, logType);

            return Json(new
            {
                sEcho = parametros.sEcho,
                iTotalRecords = parametros.TotalReg,
                iTotalDisplayRecords = parametros.DisplayReg,
                orderClasses = true,
                aaData = parametros.FinalData
            }, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// Chama a view parcial de visualização em janela modal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var entity = (id == 0 ? new Log() : new LogService().GetById(id));
            ViewBag.Titulo = ResMenu.Log + " - " + ResLabels.View;

            ViewBag.Locked = true;

            if (entity != null)
            {
                return PartialView("_Edit", entity);
            }
            ViewBag.Titulo = ResErrors.FailedOpenRecord;
            ViewBag.Mensagem = ResErrors.RecordNotFound;
            return PartialView("_ErroModal");
        }

        /// <summary>
        /// Chama a view parcial de detalhes em janela modal
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(EnumLists.ELogType type)
        {
            ViewBag.LogType = EnumLists.GetLocalizedDisplay<EnumLists.ELogType>(type.ToString());
            ViewBag.Title = ResLabels.ConfirmDelete + "?";

            ViewBag.LogDays = SystemConfigurationService.GetValue("ClearLogDays");
            return PartialView("_Delete");
        }

        /// <summary>
        /// Chama a view parcial de detalhes em janela modal
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteAction(EnumLists.ELogType logType)
        {
            var result = new LogService().Delete(logType);
            if (result.Success)
            {
                result.Message = ResLabels.SuccessfullyClearLog;
            }
            else
            {
                result.Success = false;
                result.Message = result.Message;
            }
            return Json(new { success = result.Success, message = result.Message });
        }

    }
}