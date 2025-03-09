//Excluir - Exibir os dados
$(document).on("click", ".table-delete", function () {
    var url = window.root + window.controller + "/Delete";
    var id = $(this).attr("data-id");
    url = url + "/" + id;

    clearModal("#modal-actions");

    $.ajax({
        url: url,
        type: "get",
        async: true,
        success: function (data) {
            $("#modal-actions-container").html(data);
        }
    });
});