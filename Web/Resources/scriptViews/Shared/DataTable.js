$(document).ready(function () {

    //Identifica  que o carregamento da DataTable não será automático
    var automaticLoad = true;
    if (typeof window.dtAutomaticLoad !== "undefined")
        automaticLoad = window.dtAutomaticLoad;

    if (automaticLoad)
        loadDataTable();
});

function loadDataTable(dataTableName, isWindow) {
    if (typeof dataTableName === "undefined")
        dataTableName = "#DataTable";

    if (typeof isWindow === "undefined")
        isWindow = false;

    var dataTableNameReal = dataTableName.replace("#", "");

    var serverSide = true;
    if (typeof window[dataTableNameReal + "_serverSide"] !== "undefined")
        serverSide = window[dataTableNameReal + "_serverSide"];

    var url = window.controller + "/ListTable";
    if (typeof window[dataTableNameReal + "_url"] !== "undefined")
        url = window[dataTableNameReal + "_url"];
    url = window.root + url;

    var columnDefs = [
        { "width": "160px", "targets": [-1] },
        { "sClass": "text-center", "targets": [-1] },
        { "orderable": false, "targets": [-1] }
    ];
    if (typeof window[dataTableNameReal + "_columnDefs"] !== "undefined")
        columnDefs = window[dataTableNameReal + "_columnDefs"];

    var paging = true;
    if (typeof window[dataTableNameReal + "_paging"] !== "undefined")
        paging = window[dataTableNameReal + "_paging"];

    var ordering = true;
    if (typeof window[dataTableNameReal + "_ordering"] !== "undefined")
        ordering = window[dataTableNameReal + "_ordering"];

    var searching = true;
    if (typeof window[dataTableNameReal + "_searching"] !== "undefined")
        searching = window[dataTableNameReal + "_searching"];

    var info = true;
    if (typeof window[dataTableNameReal + "_info"] !== "undefined")
        info = window[dataTableNameReal + "_info"];

    var order = [0, 'asc'];
    if (typeof window[dataTableNameReal + "_order"] !== "undefined")
        order = window[dataTableNameReal + "_order"];

    var exportTable = true;
    if (typeof window[dataTableNameReal + "_exportTable"] !== "undefined")
        exportTable = window[dataTableNameReal + "_exportTable"];

    var title = "*";
    if (typeof window[dataTableNameReal + "_title"] !== "undefined")
        title = window[dataTableNameReal + "_title"];

    var hideLastColumn = true;
    if (typeof window[dataTableNameReal + "_hideLastColumn"] !== "undefined")
        hideLastColumn = window[dataTableNameReal + "_hideLastColumn"];

    var oTableDt = $(dataTableName).DataTable({
        "processing": serverSide,
        "serverSide": serverSide,
        "sAjaxSource": serverSide ? url : null,
        "orderClasses": true, //Destaca a coluna que está ordenada
        "paging": paging,
        "ordering": ordering,
        "searching": searching,
        "info": info,
        "pagingType": "simple_numbers",
        "lengthMenu": [[10, 25, 50, 100, 500, -1], [10, 25, 50, 100, 500, "All"]],
        "iDisplayLength": 10,
        "language": {
            "url": window.root + "DataTables/Language"
        },
        "orderMulti": false,
        "order": order,
        "columnDefs": columnDefs,
        fnDrawCallback: function () {
            $(this.$("[data-toggle=tooltip]")).tooltip({
                "delay": 0,
                "track": true,
                "fade": 250,
                container: this.parent().parent()
            });
        },
        initComplete: function (settings, json) {
            if (searching) {
                var oTableId = "#" + this[0].id;
                $(oTableId + "_filter input").unbind();
                $(oTableId + "_filter input").bind("keyup", function (e) {
                    if (e.keyCode === 13) {
                        oTableDt.search(this.value).draw();
                    }
                });
            }

            if (isWindow) {
                //Para ajuste dos cabeçalhos na primeira execução
                setTimeout(function () {
                    oTableDt.draw();
                }, 250);
            }

            if (exportTable)
                dataTableButtons(oTableDt, hideLastColumn, "#" + settings.sInstance, title, settings.oFeatures.bPaginate, settings.oFeatures.bFilter);
        }
    });
}
