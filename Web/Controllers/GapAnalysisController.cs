﻿using DataServices;
using Infra;
using Localization.DataServices;
using Localization.Web;
using Model.AbsModels;
using Model.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Web.Business;

namespace Web.Controllers
{
    [Authorize]
    public class GapAnalysisController : Controller
    {
        GapAnalysisService gaAnalysisService;

        public GapAnalysisController()
        {
            gaAnalysisService = new GapAnalysisService();
        }


        public ActionResult Index()
        {
            var menuItem = MenuBuilderService.GetMenuByTitle(ResMenu.GapAnalysis);
            ViewBag.Icon = menuItem.Icon;
            ViewBag.Title = menuItem.Title;
            ViewBag.SubTitle = menuItem.Description;
            ViewBag.VideoTutorialUrl = menuItem.VideoTutorialUrl;

            return View();
        }

        public ActionResult ListTable(TableParameter parametros)
        {
            gaAnalysisService.List(ref parametros);

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
            var entity = (id == 0 ? new GapAnalysis() : gaAnalysisService.GetById(id));
            if (entity == null)
            {
                ViewBag.Titulo = ResErrors.FailedOpenRecord;
                ViewBag.Mensagem = ResErrors.RecordNotFound;
                return PartialView("_ErroModal");
            }

            ViewBag.TipoEdicao = (id == 0 ? "insert" : delete ? "delete" : "edit");
            ViewBag.Titulo = ResMenu.GapAnalysis + " - " + (id == 0 ? ResLabels.Insert : delete ? ResLabels.Delete : locked ? ResLabels.View : ResLabels.Edit);

            ViewBag.Locked = locked;

            return PartialView("_Edit", entity);
        }

        /// <summary>
        /// Chama a rotina de inclusão / alteração de registros, processando via Json
        /// </summary>
        /// <param name="entity">Objeto a ser incluído/alterado</param>
        /// <param name="oldDescription"></param>
        /// <returns></returns>
        [Authorize(Roles = GlobalFunctions.AdmProfile + "," + GlobalFunctions.GAProfile)]
        [HttpPost]
        public JsonResult EditAction(GapAnalysis entity, string oldDescription)
        {
            try
            {
                var validation = CheckUnique(entity).Data.ToString();
                if (!string.IsNullOrEmpty(validation))
                    ModelState.AddModelError(string.Empty, validation);

                if (ModelState.IsValid)
                {
                    var resultado = gaAnalysisService.Save(entity);
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
            var data = gaAnalysisService.GetById(id);
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
        [Authorize(Roles = GlobalFunctions.AdmProfile + "," + GlobalFunctions.GAProfile)]
        [HttpPost]
        public JsonResult DeleteAction(int id)
        {
            try
            {
                var result = new ResultProcessing();
                var entity = gaAnalysisService.GetById(id);
                if (entity != null)
                {
                    result = gaAnalysisService.Delete(id);
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
        public JsonResult CheckUnique(GapAnalysis entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
                return Json(true, JsonRequestBehavior.AllowGet);

            var retorno = "";
            if (gaAnalysisService.GetByFilter(p => p.Id != entity.Id && p.UserName.Equals(entity.UserName, StringComparison.OrdinalIgnoreCase)).Any())
                retorno = ResLabels.ItemExists;

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult InsertEmployees()
        {
            var result = new ResultProcessing { Success = true, Message = "Employees Synchronization successful" };
            try
            {
                var connectionStringSettings = ConfigurationManager.ConnectionStrings["DatabaseContext"];
                if (connectionStringSettings == null || string.IsNullOrWhiteSpace(connectionStringSettings.ConnectionString))
                {
                    throw new ArgumentException("The database context name cannot be null, empty or contain only white space.");
                }

                using (var context = new DbContext(connectionStringSettings.ConnectionString))
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            // Executa a procedure com o parâmetro
                            context.Database.ExecuteSqlCommand("EXEC dbo.InsertEmployees @param", new SqlParameter("@param", 3));
                            LogService.Save(EnumLists.ELogType.Service, "Insert GapAnalysis Employees " + result.Message, true, "Manual System", 1);
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                LogService.Save(EnumLists.ELogType.Service, ex.Message, true, "System", 1);
            }
            return Json(result);
        }
        public JsonResult DisableAll()
        {
            var result = new ResultProcessing { Success = true, Message = "Employees Synchronization successful" };
            try
            {
                var connectionStringSettings = ConfigurationManager.ConnectionStrings["DatabaseContext"];
                if (connectionStringSettings == null || string.IsNullOrWhiteSpace(connectionStringSettings.ConnectionString))
                {
                    throw new ArgumentException("The database context name cannot be null, empty or contain only white space.");
                }

                using (var context = new DbContext(connectionStringSettings.ConnectionString))
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            // Executa a procedure com o parâmetro
                            context.Database.ExecuteSqlCommand("EXEC dbo.EmployeesDisableAll @param", new SqlParameter("@param", 3));
                            LogService.Save(EnumLists.ELogType.Service, "Disable GapAnalysis Employees " + result.Message, true, "Manual System", 1);
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                LogService.Save(EnumLists.ELogType.Service, ex.Message, true, "System", 1);
            }
            return Json(result);
        }

        public void LoadFoos()
        {
            if (Request.Files.Count > 0)
            {
                List<string> foos = new List<string>();

                using (StreamReader reader = new StreamReader(Request.Files[0].InputStream))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        foos.Add(line);
                        LogService.Save(EnumLists.ELogType.Service, "Change Workday Employees Notification " + line, true, "Manual Update", 1);

                        // Supondo que você tenha um método para converter a linha em uma entidade Workday
                        GapAnalysis entity = ConvertLineToWorkday(line);

                        // Verifica se o EmployeeID está presente na linha lida
                        if (line.Contains(entity.EmployeeID))
                        {
                            entity.Notification = true;
                        }

                        ChangeNotification(entity);
                    }
                }

            }
            else
            {
                LogService.Save(EnumLists.ELogType.Service, "File not found Workday Employees ", true, "Manual Update", 1);
            }
        }

        public GapAnalysis ConvertLineToWorkday(string line)
        {
            GapAnalysis entity = new GapAnalysis();
            entity.EmployeeID = line; // Ajuste conforme necessário
                                      // Defina outras propriedades da entidade Workday aqui
            return entity;
        }

        public void ChangeNotification(GapAnalysis entity)
        {
            if (string.IsNullOrEmpty(entity.EmployeeID))
                return;

            var user = gaAnalysisService.GetByFilter(p => p.EmployeeID.Equals(entity.EmployeeID, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (user != null)
            {
                user.Notification = true;
                gaAnalysisService.Save(user);
            }
        }

    }
}