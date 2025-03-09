//Gravar - Ação
$(form + " #EditSubmit").on("click", function (e) {
    e.preventDefault();
    var bt = this;

    if (!$(form).valid()) return;
    $(bt).prop("disabled", true);

    var url = window.root + window.controller + "/EditAction";

    $.ajax({
        async: true,
        url: url,
        type: "POST",
        data: $(form).serialize(),
        //contentType: false,
        processData: false,
        success: function (result) {
            if (result.success) {
                $("#modal-actions").modal("hide");
                toastr['success'](result.message);
                $(dataTableName).DataTable().ajax.reload(null, false);
            } else {
                toastr["error"](result.message, ResLabels_ErrorOccurred);
                $(bt).prop("disabled", false);
            }
        }
    });
});