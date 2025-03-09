var form = "form#edit";

$(form).removeData("validator");
$(form).removeData("unobtrusiveValidation");
$.validator.unobtrusive.parse(form);


$(document).ready(function () {
    if (Locked) {
        $(form + " input:not([id='Close']), " + form + " textarea, form#edit button:not([class='close']), " + form + " select").attr("disabled", "disabled");
        $(form + " #EditSubmit").addClass("hidden");
    }
    loadRules();

    highlightRequiredFields();

    $("#modal-actions").on("shown.bs.modal", function () {
        $("input:text:visible:first", this).focus();
    });
});
