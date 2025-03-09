using Localization.Web;
using Model.AbsModels;
using Model.DatabaseContext;
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace DataServices
{
   public class NotificationService : BaseService<Notification>
    {
        /// <summary>
        /// Retorna uma lista formatada para uso com o componente Datatables
        /// </summary>
        /// <param name="parameters"></param>
        /// 

        public void List(ref TableParameter parameters)
        {

            var dadosTotal = Count();
            var parametrosLocal = parameters;

            //Filtro dos dados através do campo de busca
            Expression<Func<Notification, bool>> filter = (
                x => string.IsNullOrEmpty(parametrosLocal.sSearch) ||
                    x.Area.ToUpper().Contains(parametrosLocal.sSearch.ToUpper()) ||
                    x.Message.ToUpper().Contains(parametrosLocal.sSearch.ToUpper())
            );

            Func<Notification, object> orderingFunction;
            switch (parametrosLocal.iSortCol_0)
            {
                case "1":
                    orderingFunction = (x => x.Area);
                    break;
                case "2":
                    orderingFunction = (x => x.Message);
                    break;
                case "3":
                    orderingFunction = (x => x.Active);
                    break;
                default:
                    orderingFunction = (x => x.Area);
                    break;
            }

            var dadosFiltrados = GetByFilter(
                filter,
                orderingFunction: orderingFunction,
                orderingAsc: (parametrosLocal.sSortDir_0 == "desc"),
                skip: parametrosLocal.iDisplayStart,
                pageLength: parametrosLocal.iDisplayLength
            );

            var isAdmin = UserAuthService.RetornaUsuarioLogado().IsAdmin;
            //Gerando resultado para ser utilizado no JSon
            var dadosFinal = dadosFiltrados.Select(x => new[]
            {
                x.Area,
                x.Message,
                x.Active ?
                    "<span class='text-success'>" + ResLabels.Yes + "</span>":
                    "<span class='text-danger'>" + ResLabels.No + "</span>",
                string.Format(
                    WebButtons.MakeGroup(
                        WebButtons.ViewSm,
                        (!isAdmin ? "" : WebButtons.EditSm),
                        (!isAdmin ? "" : WebButtons.DeleteSm)
                    )
                    , x.Id.ToString(CultureInfo.CurrentCulture))
            });

            parametrosLocal.TotalReg = dadosTotal;
            parametrosLocal.FilteredData = dadosFiltrados;
            parametrosLocal.DisplayReg = Count(filter: filter);
            parametrosLocal.FinalData = dadosFinal;

            parameters = parametrosLocal;
        }
    }
}

