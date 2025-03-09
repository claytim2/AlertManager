using DataServices;
using Localization.Web;
using Model.DatabaseContext;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Web.Controllers
{
    [AllowAnonymous]
    public class AboutConfigurationController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-question-circle";
            ViewBag.Title = ResLabels.About;
            ViewBag.SubTitle = "";
            ViewBag.VideoTutorialUrl = "";

            var aboutEntity = new AboutConfigurationService().GetAll().FirstOrDefault();
            if (aboutEntity == null)
            {
                new AboutConfigurationService().Save(new AboutConfiguration());
                aboutEntity = new AboutConfigurationService().GetAll().FirstOrDefault();
            }

            return View(aboutEntity);
        }
        public ActionResult List()
        {
            var aboutEntity = new AboutConfigurationService().GetAll().FirstOrDefault();
            if (aboutEntity == null)
            {
                new AboutConfigurationService().Save(new AboutConfiguration());
                aboutEntity = new AboutConfigurationService().GetAll().FirstOrDefault();
            }

            return PartialView(aboutEntity);
        }

        /// <summary>
        /// Chama a rotina de inclusão / alteração de registros, processando via Json
        /// </summary>
        /// <param name="entity">Objeto a ser incluído/alterado</param>
        /// <param name="oldDescription"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EditAction(AboutConfiguration entity)
        {
            try
            {
                var app = new AboutConfigurationService();
                var resultado = app.Save(entity);
                if (resultado.Success)
                {
                    resultado.Message = ResLabels.SuccessfullyWrittenRecord;
                }
                return Json(new { success = resultado.Success, message = resultado.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }
    }
}