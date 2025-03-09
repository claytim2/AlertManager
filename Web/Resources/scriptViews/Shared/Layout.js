$(document).ready(function () {

    //Função para captura de eventos ajax
    $(function () {
        $.ajaxSetup({
            error: function (jqXHR, exception) {
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'Not connect.\n Verify Network.';
                } else if (jqXHR.status == 404) {
                    msg = 'Requested page not found. [404]';
                } else if (jqXHR.status == 500) {
                    msg = 'Internal Server Error [500].';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }

                //toastr["error"](msg, "AJAX ERROR");;
            }
        });
    });
});

function clearModal(modal) {
    var content = '<div class="modal-content"><div class="modal-body"><h1 class="text-center"><i class="fa fa-spin fa-spinner"></i></h1></div></div>';

    $(modal + "-container").html($.parseHTML(content));
    $(modal).modal("show");
}

$('.launch-videomodal').on('click', function (e) {
    e.preventDefault();
    var videoSrc = $(this).attr("data-videosrc");
    var modalVideo = "#modal-video";
    clearModal(modalVideo);

    var url = window.root + "Home/VideoTutorial";
    $.ajax({
        url: url,
        data: { videoSrc: videoSrc },
        type: "get",
        async: true,
        success: function (data) {
            $(modalVideo + "-container").html(data);
        }
    });

});