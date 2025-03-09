//Visualizar
$(document).on("click", ".table-detail", function () {
    var url = window.root + window.controller + "/Detail";
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
