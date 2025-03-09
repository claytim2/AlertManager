using Localization.Web;
using Model.AbsModels;
using Model.DatabaseContext;
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;



namespace DataServices
{
    /// <summary>
    /// 
    /// </summary>
   public class WorkdayService : BaseService<Workday>
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
            Expression<Func<Workday, bool>> filter = (
                x => string.IsNullOrEmpty(parametrosLocal.sSearch) ||
                    x.Name.ToUpper().Contains(parametrosLocal.sSearch.ToUpper()) ||
                    x.Registration.ToUpper().Contains(parametrosLocal.sSearch.ToUpper()) ||
                    x.EmployeeID.ToString().ToUpper().Contains(parametrosLocal.sSearch.ToUpper()) ||
                    x.UserName.ToUpper().Contains(parametrosLocal.sSearch.ToUpper())
            );

            Func<Workday, object> orderingFunction;
            switch (parametrosLocal.iSortCol_0)
            {
                case "1":
                    orderingFunction = (x => x.EmployeeID);
                    break;
                case "2":
                    orderingFunction = (x => x.Registration);
                    break;
                case "3":
                    orderingFunction = (x => x.UserName);
                    break;
                case "4":
                    orderingFunction = (x => x.Name);
                    break;
                case "5":
                    orderingFunction = (x => x.Email);
                    break;
                case "6":
                    orderingFunction = (x => x.Notification);
                    break;
                default:
                    orderingFunction = (x => x.Notification);
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
                x.EmployeeID,
                x.Registration,
                x.UserName,
                x.Name,
                x.Email,
                x.Notification ?
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
