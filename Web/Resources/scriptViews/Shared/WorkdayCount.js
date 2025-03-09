//$(document).ready(function () {
//    COLOCAR SLEEP AQUI
//    var ids = [
//        { id: "#workdayCount", resultKey: "WorkdayCount" },
//        { id: "#gapanalysisCount", resultKey: "GapAnalysisCount" },
//        { id: "#josCount", resultKey: "JosCount" },
//        { id: "#lpabelCount", resultKey: "LpaBelCount" },
//        { id: "#lpavlsCount", resultKey: "LpaVlsCount" },
//        { id: "#rhCount", resultKey: "RhCount" }
//    ];

//    ids.forEach(function (item) {
//        $(item.id).each(function () {
//            var url = window.root + window.controller + "/GetCount";
//            console.log("URL:", url);
//            $.ajax({
//                url: url,
//                type: "GET",
//                success: function (result) {
//                    console.log("Resultado:", result);
//                    if ($(item.id).length) {
//                        $(item.id).text(result[item.resultKey]);
//                    }
//                    if (item.id === "#rhCount") {
//                        toastr.success("Success Synchronize", result.message, ResLabels_SyncSuccess);
//                    }
//                },
//                error: function (xhr, status, error) {
//                    console.error("Erro:", error);
//                    if (item.id === "#rhCount") {
//                        toastr.error("Function Reload Failed", error, ResLabels_SyncFail);
//                    }
//                }
//            });
//        });
//    });
//});

$(document).ready(function () {
    var ids = [
        { id: "#workdayCount", resultKey: "WorkdayCount" },
        { id: "#gapanalysisCount", resultKey: "GapAnalysisCount" },
        { id: "#josCount", resultKey: "JosCount" },
        { id: "#lpabelCount", resultKey: "LpaBelCount" },
        { id: "#lpavlsCount", resultKey: "LpaVlsCount" },
        { id: "#rhCount", resultKey: "RhCount" }
    ];

    ids.forEach(function (item) {
        $(item.id).each(function () {
            var url = window.root + window.controller + "/GetCount";
            console.log("URL:", url);
            setTimeout(function () { // Add delay here
                $.ajax({
                    url: url,
                    type: "GET",
                    success: function (result) {
                        console.log("Resultado:", result);
                        if ($(item.id).length) {
                            $(item.id).text(result[item.resultKey]);
                        }
                        if (item.id === "#rhCount") {
                            toastr.success("Success Synchronize", result.message, ResLabels_SyncSuccess);
                        }
                    },
                    error: function (xhr, status, error) {
                        console.error("Erro:", error);
                        if (item.id === "#rhCount") {
                            toastr.error("Function Reload Failed", error, ResLabels_SyncFail);
                        }
                    }
                });
            }, 9000); // Delay in milliseconds (1000ms = 1 second)
        });
    });
});



