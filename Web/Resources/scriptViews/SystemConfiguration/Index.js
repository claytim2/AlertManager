//Variáveis para a tabela de configuração
window["DataTable_paging"] = false;
window["DataTable_searching"] = false;
window["DataTable_ordering"] = false;
window["DataTable_info"] = false;

$(document).ready(function () {
    var dataTableAgentName = "#DataTableAgent";

    window["DataTableAgent_paging"] = false;
    window["DataTableAgent_searching"] = false;
    window["DataTableAgent_ordering"] = false;
    window["DataTableAgent_info"] = false;
    window["DataTableAgent_url"] = window.controller + "/ListTableAgent";
    window["DataTableAgent_hideLastColumn"] = false;
    window["DataTableAgent_columnDefs"] = [
        { "sClass": "text-center", "targets": [1] }
    ];

    loadDataTable(dataTableAgentName);

    $(document).on("click", "#agentRefresh", function () {
        $(dataTableAgentName).DataTable().ajax.reload(null, false);
    });

    //Gravar Sobre
    $("#saveAbout").on("click", function (e) {
        var formAbout = "form#editAbout";
        e.preventDefault();
        var bt = this;

        if (!$(formAbout).valid()) return;
        $(bt).prop("disabled", true);

        var url = window.root + "AboutConfiguration/EditAction";
        $.ajax({
            url: url,
            type: "POST",
            data: $(formAbout).serialize(),
            success: function (result) {
                if (result.success) {
                    toastr['success'](result.message);
                } else {
                    toastr["error"](result.message, ResLabels_ErrorOccurred);
                }
                $(bt).prop("disabled", false);
            }
        });
    });
});
