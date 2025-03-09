using Localization.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class DataTablesController : Controller
    {
        /// <summary>
        /// Retorna para o objeto DataTables as strings de internacionalização
        /// </summary>
        /// <returns></returns>
        public JsonResult Language()
        {
            var retorno = new
            {
                oAria = new
                {
                    sSortAscending = ResDataTable.sSortAscending,
                    sSortDescending = ResDataTable.sSortDescending
                },
                oPaginate = new
                {
                    sFirst = ResDataTable.sFirst,
                    sLast = ResDataTable.sLast,
                    sNext = ResDataTable.sNext,
                    sPrevious = ResDataTable.sPrevious
                },
                sEmptyTable = ResDataTable.sEmptyTable,
                sInfo = ResDataTable.sInfo,
                sInfoEmpty = ResDataTable.sInfoEmpty,
                sInfoFiltered = ResDataTable.sInfoFiltered,
                sInfoPostFix = ResDataTable.sInfoPostFix,
                sDecimal = ResDataTable.sDecimal,
                sThousands = ResDataTable.sThousands,
                sLengthMenu = ResDataTable.sLengthMenu,
                sLoadingRecords = ResDataTable.sLoadingRecords,
                sProcessing = ResDataTable.sProcessing,
                sSearch = ResDataTable.sSearch,
                sUrl = ResDataTable.sUrl,
                sZeroRecords = ResDataTable.sZeroRecords,
                sSearchPlaceholder = ResDataTable.sSearchPlaceholder
            };
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}