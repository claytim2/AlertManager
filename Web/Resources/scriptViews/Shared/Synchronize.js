//var dataTableName = "#DataTable"; // Substitua pelo seletor correto da sua tabela


$("#Disableall").on("click", function (e) {
    e.preventDefault();

    var bt = this;
    $(bt).prop("disabled", true);

    var url = window.root + window.controller + "/DisableAll";
    $.ajax({
        url: url,
        type: "POST",
        success: function (result) {
            if (result.success) {
                toastr.success("Success Disable all", result.message, ResLabels_SyncSuccess);
                $(dataTableName).DataTable().ajax.reload(null, false);
            } else {
                setTimeout(function () {
                    location.reload();
                }, 3000);
                toastr.success("Please Waiting Synchronize", result.message, ResLabels_SyncSuccess);

            }
        },
        error: function (xhr, status, error) {
            toastr.error("Function Reload Failed", error, ResLabels_SyncFail);

        },
        complete: function () {
            // Garantir que o botão seja reabilitado em qualquer caso
            $(bt).prop("disabled", false);
        }
    });
});

$("#Update").on("click", function (e) {
    e.preventDefault();

    var bt = this;
    $(bt).prop("disabled", true);
    // toastr.success("Please Waiting Synchronize Employees", success, ResLabels_SyncSuccess);
    var url = window.root + window.controller + "/InsertEmployees";
    $.ajax({
        url: url,
        type: "POST",
        success: function (result) {
            if (result.success) {
                toastr.success("Employees Success Synchronize", result.message, ResLabels_SyncSuccess);
                $(dataTableName).DataTable().ajax.reload(null, false);
            } else {
                setTimeout(function () {
                    location.reload();
                }, 3000);
                //toastr.success("Please Waiting Synchronize Employees", result.message, ResLabels_SyncSuccess);
            }
        },
        error: function (xhr, status, error) {

            toastr.error("Function Reload Failed", error, ResLabels_SyncFail);

        },
        complete: function () {
            // Garantir que o botão seja reabilitado em qualquer caso
            $(bt).prop("disabled", false);
        }
    });
});

$("#Upload").on("click", function (e) {
    e.preventDefault();

    var bt = this;
    $(bt).prop("disabled", true);

    var formData = new FormData();
    var fileInput = document.getElementById("fileInput");
    if (fileInput.files.length > 0) {
        formData.append("file", fileInput.files[0]);
    } else {
        toastr.error("No file to upload");
        setTimeout(function () {
            location.reload();
        }, 3000); // 3000 milissegundos = 3 segundos
        return;
    }

    var url = window.root + window.controller + "/LoadFoos";
    $.ajax({
        url: url,
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,
        success: function (result) {
            if (result.success) {
                
                $(dataTableName).DataTable().ajax.reload(null, false);
                
            } else {
                setTimeout(function () {
                    location.reload();
                }, 3000);
               // toastr.success("Please Waiting Synchronize Employees", result.message, ResLabels_SyncSuccess);
                toastr.success("Employees Success Synchronize", result.message, ResLabels_SyncSuccess);
            }
        },
        error: function (xhr, status, error) {

            toastr.error("Function Reload Failed", error, ResLabels_SyncFail);

        },
        complete: function () {
            // Garantir que o botão seja reabilitado em qualquer caso
            $(bt).prop("disabled", false);
            toastr.success("Employees Success Synchronize", result.message, ResLabels_SyncSuccess);
        }
    });
});

//$("#Upload").on("click", function (e) {
//    e.preventDefault();

//    var bt = this;
//    $(bt).prop("disabled", true);

//    var formData = new FormData();
//    var fileInput = document.getElementById("fileInput");
//    if (fileInput.files.length > 0) {
//        formData.append("file", fileInput.files[0]);
//    }

//    var url = window.root + window.controller + "/LoadFoos";
//    $.ajax({
//        url: url,
//        type: "POST",
//        data: formData,
//        contentType: false,
//        processData: false,
//        success: function (result) {
//            if (result.success) {
//                //toastr.success("Employees Success Synchronize", result.message, ResLabels_SyncSuccess);
//                // Certifique-se de que dataTableName está corretamente definido
//                if ($.fn.DataTable.isDataTable(dataTableName)) {
//                    $(dataTableName).DataTable().ajax.reload(null, false);
//                } else {

//                    console.error("DataTable not found: " + dataTableName);
//                }
//            } else {
//                setTimeout(function () {
//                    location.reload();
//                }, 3000);
//            }
//        },
//        error: function (xhr, status, error) {
//            toastr.error("Function Reload Failed", error, ResLabels_SyncFail);
//        },
//        complete: function () {
//            $(bt).prop("disabled", false);
//        }
//    });
//});

//$("#Upload").on("click", function (e) {
//    e.preventDefault();

//    var bt = this;
//    $(bt).prop("disabled", true);

//    var formData = new FormData();
//    var fileInput = document.getElementById("fileInput");
//    if (fileInput.files.length > 0) {
//        formData.append("file", fileInput.files[0]);
//    }

//    var url = window.root + window.controller + "/LoadFoos";
//    $.ajax({
//        url: url,
//        type: "POST",
//        data: formData,
//        contentType: false,
//        processData: false,
//        success: function (result) {
//            if (result.success) {
//               // toastr.success("Employees Success Synchronize", result.message, ResLabels_SyncSuccess);
//                $(dataTableName).DataTable().ajax.reload(null, false);
//            } else {
//                setTimeout(function () {
//                    location.reload();
//                }, 3000);
//            }
//        },
//        error: function (xhr, status, error) {
//            toastr.error("Function Reload Failed", error, ResLabels_SyncFail);
//        },
//        complete: function () {
//            $(bt).prop("disabled", false);
//        }
//    });
//});

