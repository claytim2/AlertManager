var dataTableName = "#DataTable";
var form = "form#edit";

$(form).removeData("validator");
$(form).removeData("unobtrusiveValidation");
$.validator.unobtrusive.parse(form);
