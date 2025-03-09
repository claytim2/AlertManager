//Combo de Usuários
function Select2Employee(identifier, allowClear) {
    if (allowClear == null)
        allowClear = true;

    $(identifier).select2({
        placeholder: window.ResLabelsSelectValue,
        language: window.culture,
        allowClear: allowClear,
        ajax: {
            url: function () { return window.root + "JbelUser/Select2Format/" },
            dataType: "json",
            delay: 250,
            data: function (params) {
                return {
                    q: params.term, // search term
                    pagesize: 10,
                    page: params.page
                };
            },
            processResults: function (data, params) {
                params.page = params.page || 1;
                return {
                    results: data.Results,
                    pagination: {
                        more: (params.page * 10) < data.Total
                    }
                };
            },
            cache: true
        },
        initSelection: function (element, callback) {
            var elementValue = $(element).attr("data-init-value");
            var elementText = $(element).attr("data-init-text");
            callback({ "text": elementText, "id": elementValue });
        },
        escapeMarkup: function (markup) { return markup; },
        minimumInputLength: 1
    })
        .on("change", function () {

        });
}