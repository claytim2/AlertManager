using DataServices;
using Localization.DataServices;
using Localization.Web;
using Model.AbsModels;
using Model.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Business;

namespace Web.Controllers
{
    [Authorize]
    public class LocationController : Controller
    {
        LocationService locationService;
        public LocationController()
        {
            locationService = new LocationService();
        }
        public ActionResult Index()
        {
            var menuItem = MenuBuilderService.GetMenuByTitle(ResMenu.Location);
            ViewBag.Icon = menuItem.Icon;
            ViewBag.Title = menuItem.Title;
            ViewBag.SubTitle = menuItem.Description;
            ViewBag.VideoTutorialUrl = menuItem.VideoTutorialUrl;

            return View();
        }

        public ActionResult ListTable(TableParameter parametros)
        {
            locationService.List(ref parametros);

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
        /// Chama a view parcial de inclusão / alteração em janela modal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="locked"></param>
        /// <param name="delete"></param>
        /// <returns></returns>
        [HttpGet]

        public ActionResult Edit(int id, bool locked = false, bool delete = false)
        {
            var entity = (id == 0 ? new Location() : locationService.GetById(id));
            if (entity == null)
            {
                ViewBag.Titulo = ResErrors.FailedOpenRecord;
                ViewBag.Mensagem = ResErrors.RecordNotFound;
                return PartialView("_ErroModal");
            }

            ViewBag.TipoEdicao = (id == 0 ? "insert" : delete ? "delete" : "edit");
            ViewBag.Titulo = ResMenu.Location + " - " + (id == 0 ? ResLabels.Insert : delete ? ResLabels.Delete : locked ? ResLabels.View : ResLabels.Edit);
            ViewBag.Locked = locked;

            return PartialView("_Edit", entity);
        }

        /// <summary>
        /// Chama a rotina de inclusão / alteração de registros, processando via Json
        /// </summary>
        /// <param name="entity">Objeto a ser incluído/alterado</param>
        /// <param name="oldDescription"></param>
        /// <returns></returns>
        [Authorize(Roles = GlobalFunctions.AdmProfile)]
        [HttpPost]
        public JsonResult EditAction(Location entity, string oldDescription)
        {
            try
            {
                var validation = CheckUnique(entity).Data.ToString();
                if (!string.IsNullOrEmpty(validation))
                    ModelState.AddModelError(string.Empty, validation);

                if (ModelState.IsValid)
                {
                    var resultado = locationService.Save(entity);
                    if (resultado.Success)
                    {
                        resultado.Message = ResLabels.SuccessfullyWrittenRecord;
                    }
                    return Json(new { success = resultado.Success, message = resultado.Message, newId = (int)resultado.AdditionalData });
                }

                var mensagem = "";
                foreach (var erro in ModelState.Values.SelectMany(item => item.Errors))
                {
                    if (mensagem != "") mensagem += " / ";
                    mensagem += erro.ErrorMessage;
                }

                return Json(new { success = false, message = mensagem });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

        /// <summary>
        /// Chama a view parcial de detalhes em janela modal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            return Edit(id, true);
        }

        /// <summary>
        /// Chama a view parcial de detalhes em janela modal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            var data = locationService.GetById(id);
            if (data == null)
                ViewBag.Mensagem = ResErrors.RecordNotFound;
            else
            {
                return Edit(id, true, true);
            }

            ViewBag.Titulo = ResErrors.FailedOpenRecord;
            return PartialView("_ErroModal");
        }

        /// <summary>
        /// Exclui um registro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = GlobalFunctions.AdmProfile)]
        [HttpPost]
        public JsonResult DeleteAction(int id)
        {
            try
            {
                var result = new ResultProcessing();
                var entity = locationService.GetById(id);
                if (entity != null)
                {
                    result = locationService.Delete(id);
                    if (result.Success)
                    {
                        result.Message = ResLabels.SuccessfullyDeletedRecord;
                    }
                }
                else
                {
                    result.Success = false;
                    result.Message = ResErrors.RecordNotFound;
                }
                return Json(new { success = result.Success, message = result.Message });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }

        }

        /// <summary>
        /// Valida duplicidade de valor
        /// </summary>
        /// <param name="description">Valor a validar</param>
        /// <returns></returns>
        public JsonResult CheckUnique(Location entity)
        {
            if (string.IsNullOrEmpty(entity.Area))
                return Json(true, JsonRequestBehavior.AllowGet);

            var retorno = "";
            if (locationService.GetByFilter(p => p.Id != entity.Id && p.Area.Equals(entity.Area, StringComparison.OrdinalIgnoreCase)).Any())
                retorno = ResLabels.ItemExists;

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}