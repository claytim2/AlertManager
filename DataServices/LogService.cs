using DataAccess.Context;
using Infra;
using Localization.DataServices;
using Localization.Web;
using Model.AbsModels;
using Model.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataServices
{
    public class LogService : BaseService<Log>
    {
        public static bool Save(EnumLists.ELogType logType, string description = "", bool status = true, string userName = "", int siteId = 0)
        {

            try
            {
                var user = UserAuthService.RetornaUsuarioLogado();
                if (string.IsNullOrEmpty(user.UserName))
                    user.UserName = "Automatic Job";

                var log = new Log
                {
                    DateTime = DateTime.Now,
                    Description = description,//(description.Length > 2000 ? description.Substring(0, 2000) : description),
                    IpAddress = GlobalFunctions.GetIpAddress(),
                    LogType = logType,
                    Status = status,
                    UserName = string.IsNullOrEmpty(userName) ? user.UserName : userName,
                    SiteId = siteId == 0 ? GlobalFunctions.SiteId : siteId,
                    DateCreate = DateTime.Now,
                    UserCreate = string.IsNullOrEmpty(userName) ? user.UserName : userName
                };

                var app = new LogService();
                app.Save(log);
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }

        /// <summary>
        /// Retorna uma lista formatada para uso com o componente Datatables
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="logType"></param>
        public void List(ref TableParameter parameters, EnumLists.ELogType logType)
        {

            var dadosTotal = Count(filter: (t => t.LogType == logType));
            var parametrosLocal = parameters;
            //var sSearchSemAcento = parametrosLocal.sSearch.RemoverAcentos();

            //Filtro dos dados através do campo de busca
            Expression<Func<Log, bool>> filter = (x => x.LogType == logType &&
                (string.IsNullOrEmpty(parametrosLocal.sSearch) ||
                    x.Description.ToUpper().Contains(parametrosLocal.sSearch.ToUpper()) ||
                        (
                        (SqlFunctions.DatePart("dd", x.DateTime) < 10 ? "0" : "") +
                        SqlFunctions.StringConvert((double)SqlFunctions.DatePart("dd", x.DateTime)).Trim() + "/" +
                        (SqlFunctions.DatePart("MM", x.DateTime) < 10 ? "0" : "") +
                        SqlFunctions.StringConvert((double)SqlFunctions.DatePart("MM", x.DateTime)).Trim() + "/" +
                        SqlFunctions.StringConvert((double)SqlFunctions.DatePart("yyyy", x.DateTime)).Trim()
                    ).Trim().Contains(parametrosLocal.sSearch.ToUpper()) ||
                    x.IpAddress.ToUpper().Contains(parametrosLocal.sSearch.ToUpper()) ||
                    x.UserName.ToUpper().Contains(parametrosLocal.sSearch.ToUpper())
                ));

            Func<Log, object> orderingFunction;
            switch (parametrosLocal.iSortCol_0)
            {
                case "1":
                    orderingFunction = (x => x.Description + x.DateTime);
                    break;
                case "2":
                    orderingFunction = (x => x.IpAddress + x.DateTime);
                    break;
                case "3":
                    orderingFunction = (x => x.UserName + x.DateTime);
                    break;
                case "4":
                    orderingFunction = (x => x.Status.ToString() + x.DateTime);
                    break;
                default:
                    orderingFunction = (x => x.DateTime);
                    break;
            }

            var dadosFiltrados = GetByFilter(
                filter,
                orderingFunction: orderingFunction,
                orderingAsc: (parametrosLocal.sSortDir_0 == "asc"),
                skip: parametrosLocal.iDisplayStart,
                pageLength: parametrosLocal.iDisplayLength);

            //Gerando resultado para ser utilizado no JSon
            //var dadosFiltradosLista = dadosFiltrados as IList<Stock> ?? dadosFiltrados.ToList();
            var dadosFinal = dadosFiltrados.Select(x => new[]
            {
                string.Format("{0:dd/MM/yyyy HH:mm:ss}", x.DateTime),
                (GlobalFunctions.RemoveHtmlTags(x.Description).Length > 200 ? GlobalFunctions.RemoveHtmlTags(x.Description).Substring(0, 200) + string.Format("... <a class='table-detail' data-id='{0}' href='javascript:void(0)' data-toggle='tooltip' title='" + ResLabels.ViewCompleteDescription + "'><i class='fa fa-plus-square-o'></i></a>", x.Id) : GlobalFunctions.RemoveHtmlTags(x.Description)),
                x.IpAddress,
                x.UserName,
                x.Status ? "<i class='text-success fa fa-check'></i>" : "<i class='text-danger fa fa-times'></i>"
            });
            //if (parametrosLocal.iDisplayLength > -1)
            //    dadosFinal = dadosFinal.Skip(parametrosLocal.iDisplayStart).Take(parametrosLocal.iDisplayLength);

            parametrosLocal.TotalReg = dadosTotal;
            parametrosLocal.FilteredData = dadosFiltrados;
            parametrosLocal.DisplayReg = Count(filter: filter);
            parametrosLocal.FinalData = dadosFinal;

            parameters = parametrosLocal;
        }

        /// <summary>
        /// Exclui um registro
        /// </summary>
        /// <param name="logType"></param>
        /// <returns></returns>
        public ResultProcessing Delete(EnumLists.ELogType? logType = null, int siteId = 0)
        {
            if (siteId == 0)
                siteId = GlobalFunctions.SiteId;

            var todayDate = DateTime.Now.AddDays(1).Date.AddSeconds(-1);
            var logDays = int.Parse(SystemConfigurationService.GetValue("ClearLogDays"));
            var result = new ResultProcessing { Success = true };
            try
            {
                var entity = GetByFilter(t =>
                    t.SiteId == siteId &&
                    (logType == null || t.LogType == logType) &&
                    t.DateTime <= SqlFunctions.DateAdd("day", logDays * -1, todayDate)
                    , filterSite: false);

                if (!entity.Any())
                {
                    result.Success = false;
                    result.Message = ResErrors.RecordNotFoundDelete;
                    return result;
                }
                result.Message = string.Format(ResLabels.LogCleared, "All", logDays);
                DeleteRange(entity.ToList());
                if (logType != null)
                    Save(EnumLists.ELogType.DeleteRecord,
                        string.Format(ResLabels.LogCleared, EnumLists.GetLocalizedDisplay<EnumLists.ELogType>(logType.ToString()), logDays));
                return result;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
                return result;
            }
        }

    }
}
