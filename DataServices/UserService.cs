using Localization.Web;
using Model.AbsModels;
using Model.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataServices
{
    public class UserService : BaseService<Users>
    {
        /// <summary>
        /// Retorna uma lista formatada para uso com o componente Datatables
        /// </summary>
        /// <param name="parameters"></param>
        public void List(ref TableParameter parameters)
        {

            var dadosTotal = Count();
            var parametrosLocal = parameters;

            //Filtro dos dados através do campo de busca
            Expression<Func<Users, bool>> filter = (
                x => string.IsNullOrEmpty(parametrosLocal.sSearch) ||
                    x.Name.ToUpper().Contains(parametrosLocal.sSearch.ToUpper())
            );

            Func<Users, object> orderingFunction;
            switch (parametrosLocal.iSortCol_0)
            {
                case "1":
                    orderingFunction = (x => x.UserName);
                    break;
                case "2":
                    orderingFunction = (x => x.Registration);
                    break;
                case "3":
                    orderingFunction = (x => x.Active);
                    break;
                case "4":
                    orderingFunction = (x => x.Roles);
                    break;
                default:
                    orderingFunction = (x => x.Name);
                    break;
            }

            var dadosFiltrados = GetByFilter(
                filter,
                orderingFunction: orderingFunction,
                orderingAsc: (parametrosLocal.sSortDir_0 == "asc"),
                skip: parametrosLocal.iDisplayStart,
                pageLength: parametrosLocal.iDisplayLength
            );

            var isAdmin = UserAuthService.RetornaUsuarioLogado().IsAdmin;
            //Gerando resultado para ser utilizado no JSon
            var dadosFinal = dadosFiltrados.Select(x => new[]
            {
                x.Name,
                x.UserName,
                x.Registration,
                x.Active ?
                    "<span class='text-success'>" + ResLabels.Yes + "</span>":
                    "<span class='text-danger'>" + ResLabels.No + "</span>",
                x.Roles,
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
