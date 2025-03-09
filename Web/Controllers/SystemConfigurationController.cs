using DataServices;
using Infra;
using Localization.DataServices;
using Localization.Web;
using Model.AbsModels;
using Model.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Web.Business;

namespace Web.Controllers
{
    [Authorize(Roles = GlobalFunctions.AdmProfile + "," + GlobalFunctions.WdProfile+ "," + GlobalFunctions.JsProfile + "," + GlobalFunctions.GAProfile + "," + GlobalFunctions.LpaBProfile + "," + GlobalFunctions.LpaVProfile + "," + GlobalFunctions.RhProfile)]
    public class SystemConfigurationController : Controller
    {
        public ActionResult Index()
        {
            var menuItem = MenuBuilderService.GetMenuByTitle(ResMenu.Configuration);
            ViewBag.Icon = menuItem.Icon;
            ViewBag.Title = menuItem.Title;
            ViewBag.SubTitle = menuItem.Description;
            ViewBag.VideoTutorialUrl = menuItem.VideoTutorialUrl;

            new SystemConfigurationService().CheckSiteConfiguration();

            return View();
        }

        /// <summary>
        /// Método de consulta chamado pela datatable no preenchimento da tabela por ajax
        /// </summary>
        /// <param name="parametros">Parâmetros oriundos do datatable</param>
        /// <returns></returns>
        public ActionResult ListTable(TableParameter parametros)
        {
            new SystemConfigurationService().List(ref parametros);

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
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id, bool locked = false)
        {
            var entity = (id == 0 ? new SystemConfiguration() : new SystemConfigurationService().GetById(id));
            ViewBag.TipoEdicao = (id == 0 ? "insert" : "edit");
            ViewBag.Titulo = ResMenu.Configuration + " - " + (id == 0 ? ResLabels.Insert : locked ? ResLabels.View : ResLabels.Edit);

            ViewBag.Locked = locked;

            if (entity != null)
            {
                var configurationType = new SystemConfiguration().GetDefaultValues().First(p => p.Key == entity.Key);

                if (configurationType.ConfigurationType == EnumLists.EConfigurationType.Language)
                {
                    ViewBag.ConfigItems = new List<SelectListItem>
                    {
                        new SelectListItem{
                            Text = "pt-br",
                            Value = "pt-br",
                            Selected = entity.Value=="pt-br"
                        },
                        new SelectListItem{
                            Text = "en",
                            Value = "en",
                            Selected = entity.Value == "en"
                        }
                    };
                }

                if (configurationType.ConfigurationType == EnumLists.EConfigurationType.Switch)
                {
                    ViewBag.ConfigItems = new List<SelectListItem>
                    {
                        new SelectListItem{
                            Text = ResLabels.Enabled,
                            Value = "1",
                            Selected = entity.Value=="1"
                        },
                        new SelectListItem{
                            Text = ResLabels.Disabled,
                            Value = "0",
                            Selected = entity.Value=="0"
                        }
                    };
                }


                return PartialView("_Edit", entity);
            }
            ViewBag.Titulo = ResErrors.FailedOpenRecord;
            ViewBag.Mensagem = ResErrors.RecordNotFound;
            return PartialView("_ErroModal");
        }

        /// <summary>
        /// Chama a rotina de inclusão / alteração de registros, processando via Json
        /// </summary>
        /// <param name="entity">Objeto a ser incluído/alterado</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EditAction(SystemConfiguration entity)
        {
            //Verificando se é numérico para algumas configurações
            var onlyNumbers = new SystemConfiguration().GetDefaultValues()
                .Where(p => p.ConfigurationType == EnumLists.EConfigurationType.Number)
                .Select(p => p.Key);

            var languageKeys = new SystemConfiguration().GetDefaultValues()
                .Where(p => p.ConfigurationType == EnumLists.EConfigurationType.Language)
                .Select(p => p.Key);

            if (onlyNumbers.Contains(entity.Key))
            {
                if (!entity.Value.All(char.IsDigit))
                    ModelState.AddModelError(string.Empty, ResErrors.OnlyNumbers);
            }

            if (languageKeys.Contains(entity.Key))
            {
                try
                {
                    CultureInfo.GetCultureInfo(entity.Value);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, ResErrors.InvalidCulture);
                }
            }
            
            if (ModelState.IsValid)
            {
                var app = new SystemConfigurationService();
                var resultado = app.Save(entity);
                //recarregando o registro
                if (resultado.Success)
                {
                    resultado.Message = ResLabels.SuccessfullyWrittenRecord;
                }
                return Json(new { success = resultado.Success, message = resultado.Message });
            }

            var mensagem = "";
            foreach (var erro in ModelState.Values.SelectMany(item => item.Errors))
            {
                if (mensagem != "") mensagem += " / ";
                mensagem += erro.ErrorMessage;
            }

            return Json(new { success = false, message = mensagem });
        }
    }
}