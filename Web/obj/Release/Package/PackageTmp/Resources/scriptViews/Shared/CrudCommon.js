$(document).ready(function () {
    
    if (Locked) {
        $(form + " input:not([id='Cancel']):not([class='close']), " + form + " button:not([class='close']):not([id='Cancel']), " + form + " textarea, " + form + " select").attr("disabled", "disabled");
        $(form + " #EditSubmit").addClass("hidden");

    }

    if (TipoEdicao === "delete") {
        $(form + " #DeleteSubmit").removeClass("hidden");
        $(form + " #DeleteSubmit").removeAttr("disabled");
    }
    if (!window.loadrules) {
        loadRules();
    }

    highlightRequiredFields(null, null, form);

    $("#modal-actions").on("shown.bs.modal", function () {
        //var firstController = $(this).find('input[type=text],textarea,select').filter(':visible:first');
        //firstController.focus();

        //$("input:visible:first", this).focus();
    });

    //Validação de número com decimais no Jquery Validation
    if (window.culture == "pt-BR") {
        $.validator.methods.number = function (value, element) {
            return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
        }
    }
});