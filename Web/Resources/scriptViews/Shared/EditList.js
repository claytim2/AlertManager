$(document).on("click", ".table-edit", function () {
    var url = window.root + window.controller + "/Edit";
    var id = $(this).attr("data-id");
    url = url + "/" + id;

    clearModal("#modal-actions");

    $.ajax({
        url: url,
        type: "get",
        async: true,
        success: function (data) {
            $("#modal-actions-container").html(data);
            fetchData(); // Chama a função fetchData após carregar o conteúdo do modal
        },
        error: function (xhr, status, error) {
            console.error('Erro ao carregar o conteúdo do modal:', error);
        }
    });
});
