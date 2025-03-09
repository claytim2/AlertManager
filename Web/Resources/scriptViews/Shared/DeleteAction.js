//Excluir - Ação
$(form + " #DeleteSubmit").on("click", function (e) {
    e.preventDefault();
    var bt = this;
    $(bt).prop("disabled", true);

    var url = window.root + window.controller + "/DeleteAction";
    var id = $(form + " #Id").val();

    $.ajax({
        url: url,
        type: "POST",
        async: true,
        dataType: "json",
        data: { id: id },
        success: function (result) {
            if (result.success) {
                toastr['success'](result.message);
            } else {
                toastr["error"](result.message, ResLabels_ErrorOccurred);
            }
            $("#modal-actions").modal("hide");
            //Atualizando a tabela, mantendo a paginação atual
            $(dataTableName).DataTable().ajax.reload(null, false);
        }
    });
});