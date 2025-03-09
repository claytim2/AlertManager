$(document).ready(function () {
    var dataTableName = "#DataTable";

    var oTableDt = $(dataTableName).DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": window.root + window.controller + "/ListTable",
            "type": "POST",
            "data": function (d) {
                d.logType = $.url().param("Type");
            }
        },
        "orderClasses": true, //Destaca a coluna que está ordenada
        "pagingType": "simple_numbers",
        "iDisplayLength": 25,
        "language": {
            "url": window.root + "DataTables/Language"
        },
        "orderMulti": false,
        //"order": [ 0, 'asc' ],
        "columnDefs": [
            { "width": "160px", "targets": [-1] },
            { "sClass": "text-center", "targets": [0, -1, -2] },
            { "width": "150px", "targets": [0] },
            { "orderable": false, "targets": [-1] }
        ],
        "order": [[0, "desc"]],
        fnDrawCallback: function () {
            $(this.$("[data-toggle=tooltip]")).tooltip({
                "delay": 0,
                "track": true,
                "fade": 250,
                container: this.parent().parent()
            });
        },
        initComplete: function (settings, json) {
            $(dataTableName + "_filter input").unbind();
            $(dataTableName + "_filter input").bind("keyup", function (e) {
                if (e.keyCode === 13) {
                    oTableDt.search(this.value).draw();
                }
            });
        }
    });

});

//Visualizar
$(document).on("click", ".table-detail", function () {
    var url = window.root + window.controller + "/Detail";
    var id = $(this).attr("data-id");
    clearModal("#modal-actions");
    $.get(url + "/" + id, function (data) {
        $("#modal-actions-container").html(data);
        $("#modal-actions").modal("show");
    });
});

//Excluir - Exibir os dados
$(document).on("click", ".table-delete", function () {
    var url = window.root + window.controller + "/Delete?type=" + $.url().param("Type");
    clearModal("#modal-actions");
    $.get(url, function (data) {
        $("#modal-actions-container").html(data);
        $("#modal-actions").modal("show");
    });
});