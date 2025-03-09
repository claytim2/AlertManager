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
    public class NotificationController : Controller
    {
        NotificationService notificationService;

        public NotificationController()
        {
            notificationService = new NotificationService();
        }
        public ActionResult Index()
        {
            var menuItem = MenuBuilderService.GetMenuByTitle(ResMenu.Notification);
            ViewBag.Icon = menuItem.Icon;
            ViewBag.Title = menuItem.Title;
            ViewBag.SubTitle = menuItem.Description;
            ViewBag.VideoTutorialUrl = menuItem.VideoTutorialUrl;
            return View();
        }



        [HttpGet]
        public ActionResult ListTable(TableParameter parametros)
        {
            notificationService.List(ref parametros);

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
            var entity = (id == 0 ? new Notification() : notificationService.GetById(id));
            if (entity == null)
            {
                ViewBag.Titulo = ResErrors.FailedOpenRecord;
                ViewBag.Mensagem = ResErrors.RecordNotFound;
                return PartialView("_ErroModal");
            }

            ViewBag.TipoEdicao = (id == 0 ? "insert" : delete ? "delete" : "edit");
            ViewBag.Titulo = ResMenu.Notification + " - " + (id == 0 ? ResLabels.Insert : delete ? ResLabels.Delete : locked ? ResLabels.View : ResLabels.Edit);

            ViewBag.Locked = locked;
            ViewBag.AreasList = GetAreasList();

            return PartialView("_Edit", entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetAreasList()
        {
            var locationService = new LocationService();
            var areas = locationService.GetAllAreas();
            if (areas == null)
            {
                throw new Exception("GetAllAreas() retornou nulo.");
            }
            return areas.Select(area => new SelectListItem
            {
                Text = area,
                Value = area
            }).ToList();
        }

        /// <summary>
        /// Chama a rotina de inclusão / alteração de registros, processando via Json
        /// </summary>
        /// <param name="entity">Objeto a ser incluído/alterado</param>
        /// <param name="oldDescription"></param>
        /// <returns></returns>
        [Authorize(Roles = GlobalFunctions.AdmProfile)]
        [HttpPost]
        public JsonResult EditAction(Notification entity, string oldDescription)
        {
            try
            {
                var validation = CheckUnique(entity).Data.ToString();
                if (!string.IsNullOrEmpty(validation))
                    ModelState.AddModelError(string.Empty, validation);

                if (ModelState.IsValid)
                {
                    var resultado = notificationService.Save(entity);
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
            var data = notificationService.GetById(id);
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
                var entity = notificationService.GetById(id);
                if (entity != null)
                {
                    result = notificationService.Delete(id);
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
        public JsonResult CheckUnique(Notification entity)
        {
            if (string.IsNullOrEmpty(entity.Area))
                return Json(true, JsonRequestBehavior.AllowGet);

            var retorno = "";
            if (notificationService.GetByFilter(p => p.Id != entity.Id && p.Area.Equals(entity.Area, StringComparison.OrdinalIgnoreCase)).Any())
                retorno = ResLabels.ItemExists;

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}