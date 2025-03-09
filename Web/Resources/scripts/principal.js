$(function ($) {
    if ($.AdminLTE) {
        var $body = $("body");

        // On click, capture state and save it in localStorage
        $($.AdminLTE.options.sidebarToggleSelector).click(function () {
            Cookies.set("sidebar", $body.hasClass("sidebar-collapse") ? 1 : 0, { path: "LPA" });

        });


        // On ready, read the set state and collapse if needed
        if (Cookies.get("sidebar") === "1") {
            $body.addClass("disable-animations sidebar-collapse");
            requestAnimationFrame(function () {
                $body.removeClass("disable-animations");
            });
        }
    }
});


var isNN = (navigator.appName.indexOf("Netscape") != -1);
function autoTab(input, len, e, igonedChar) {
    var keyCode = (isNN) ? e.which : e.keyCode;
    var filter = (isNN) ? [0, 8, 9] : [0, 8, 9, 16, 17, 18, 37, 38, 39, 40, 46];

    if (input.value.length >= len && !containsElement(filter, keyCode) && (igonedChar == null || !(input.value.indexOf(igonedChar) > -1))) {
        input.value = input.value.slice(0, len);
        input.form[(getIndex(input) + 1) % input.form.length].focus();
    }
    function containsElement(arr, ele) {
        var found = false, index = 0;
        while (!found && index < arr.length)
            if (arr[index] == ele)
                found = true;
            else
                index++;
        return found;
    }
    function getIndex(input) {
        var index = -1, i = 0, found = false;
        while (i < input.form.length && index == -1)
            if (input.form[i] == input) index = i;
            else i++;
        return index;
    }
    return true;
}

//Desabilita o envio do formulário com Enter
function loadRules() {

    $("input").on("keypress", function (e) {
        /* ENTER PRESSED*/
        if (e.keyCode == 13) {
            /* FOCUS ELEMENT */
            var inputs = $(this).parents("form").eq(0).find(":input");
            var idx = inputs.index(this);

            if (idx == inputs.length - 1) {
                if (inputs[0])
                    inputs[0].select();
            } else {
                if (inputs[idx + 1]) {
                    inputs[idx + 1].focus(); //  handles submit buttons
                    //inputs[idx + 1].select();
                }
            }
            return false;
        }
        return true;
    });

    $('[data-tooltip="true"]').tooltip();

    //Minimal scheme for iCheck
    $('input[type="checkbox"].icheck-minimal-scheme, input[type="radio"].icheck-minimal-scheme').iCheck({
        checkboxClass: "icheckbox_minimal-blue",
        radioClass: "iradio_minimal-blue"
    });
    //Flat scheme for iCheck
    $('input[type="checkbox"].icheck-flat-scheme, input[type="radio"].icheck-flat-scheme').iCheck({
        checkboxClass: "icheckbox_flat-blue",
        radioClass: "iradio_flat-blue"
    });
    //Square scheme for iCheck
    $('input[type="checkbox"].icheck-square-scheme, input[type="radio"].icheck-square-scheme').iCheck({
        checkboxClass: "icheckbox_square-blue",
        radioClass: "iradio_square-blue"
    });

    //Line scheme for iCheck
    $('input[type="checkbox"].icheck-line-scheme, input[type="radio"].icheck-line-scheme').each(function () {
        var self = $(this),
            label = self.prev(),
            labeltext = label.text();

        label.remove();
        self.iCheck({
            checkboxClass: "icheckbox_line-blue",
            radioClass: "iradio_line-blue",
            insert: '<div class="icheck_line-icon"></div>' + labeltext
        });
    });

    //Bootbox Localization
    bootbox.addLocale("pt-br",
    {
        OK: "OK",
        CANCEL: "Cancelar",
        CONFIRM: "OK"
    });
    bootbox.setDefaults({ locale: window.culture.toLowerCase() });



}

/*
    Realiza o destaque dos campos obrigatórios, colocando um asterisco no label e deixando o input vermelho
    exception: array com os campos que não serão destacados, mesmo que obrigatórios
*/
function highlightRequiredFields(exception, force, form) {
    var formPre = "";

    if (form) {
        var formId = form.split("#");
        if (formId.length === 2) {
            formPre = form + " ";
            form = formId[1];
        } else {
            formPre = "form#" + form + " ";
        }
    }

    $(formPre + "input, " + formPre + "textarea, " + formPre + "select").not(":input[type=button], :input[type=submit], :input[type=reset]").each(function () {
        if (($(this).prop("type") !== "checkbox" &&
            this.id !== "" &&
            (this.form.id === form || !form) &&
            $(this).attr("data-val") === "true" &&
            $(this).attr("data-val-required") != undefined &&
            $.inArray(this.id, exception) === -1) ||
            $.inArray(this.id, force) > -1) {

            $(formPre + "label[for='" + this.id + "']").html($(formPre + "label[for='" + this.id + "']").text() + ' <span class="text-danger">*</span>');
            $(this).addClass("requiredField");
        }
    });
}

/*
    Escurece um modal para mostrar outro por cima
*/
$(document)
  .on("show.bs.modal", ".modal", function (event) {
      $(this).appendTo($("body"));
      var firstController = $(this).find('input[type=text],textarea,select').filter(':visible:first');
      firstController.focus();
      //$("input:text:visible:first", this).focus();
  })
  .on("shown.bs.modal", ".modal.in", function (event) {
      setModalsAndBackdropsOrder();
      var firstController = $(this).find('input[type=text],textarea,select').filter(':visible:first');
      firstController.focus();
      //$("input:text:visible:first", this).focus();
  })
  .on("hidden.bs.modal", ".modal", function (event) {
      setModalsAndBackdropsOrder();
  });

function setModalsAndBackdropsOrder() {
    var modalZIndex = 1040;
    $(".modal.in").each(function (index) {
        var $modal = $(this);
        modalZIndex++;
        $modal.css("zIndex", modalZIndex);
        $modal.next(".modal-backdrop.in").addClass("hidden").css("zIndex", modalZIndex - 1);
    });
    $(".modal.in:visible:last").focus().next(".modal-backdrop.in").removeClass("hidden");
}

function formatMoney(valor) {

    var inteiro = null, decimal = null, c = null, j = null;
    var aux = new Array();
    valor = "" + valor;
    c = valor.indexOf(".", 0);
    //encontrou o ponto na string
    if (c > 0) {
        //separa as partes em inteiro e decimal
        inteiro = valor.substring(0, c);
        decimal = valor.substring(c + 1, valor.length);
    } else {
        inteiro = valor;
    }

    //pega a parte inteiro de 3 em 3 partes
    for (j = inteiro.length, c = 0; j > 0; j -= 3, c++) {
        aux[c] = inteiro.substring(j - 3, j);
    }

    //percorre a string acrescentando os pontos
    inteiro = "";
    for (c = aux.length - 1; c >= 0; c--) {
        inteiro += aux[c] + ".";
    }
    //retirando o ultimo ponto e finalizando a parte inteiro

    inteiro = inteiro.substring(0, inteiro.length - 1);

    decimal = parseInt(decimal);
    if (isNaN(decimal)) {
        decimal = "00";
    } else {
        decimal = "" + decimal;
        if (decimal.length === 1) {
            decimal = "0" + decimal;
        }
    }
    valor = inteiro + "," + decimal;
    return valor;

}

function getUrlParameter(sParam) {
    //var sPageUrl = window.location.search.substring(1);
    //var sUrlVariables = sPageUrl.split('&');
    //for (var i = 0; i < sUrlVariables.length; i++) {
    //    var sParameterName = sUrlVariables[i].split('=');
    //    if (sParameterName[0] == sParam) {
    //        return sParameterName[1];
    //    }
    //}
    //return '';

    var results = new RegExp("[\?&]" + name + "=([^&#]*)").exec(window.location.href);
    if (results == null) {
        return null;
    }
    else {
        return results[1] || 0;
    }
}

bootbox.addLocale("pt-BR",
{
    OK: "OK",
    CANCEL: "Cancelar",
    CONFIRM: "OK"
});

//Disponibiliza o título de uma Datatable
$.fn.dataTable.Api.register("column().title()", function () {
    var colheader = this.header();
    return $(colheader).text().trim();
});

//Botões da Datatable
function dataTableButtons(oTable, hideLastColumn, tableName, title, paging, searching) {

    hideLastColumn = typeof hideLastColumn !== "undefined" ? hideLastColumn : false;
    title = typeof title !== "undefined" ? title : "*";
    tableName = typeof tableName !== "undefined" ? tableName : "#DataTable";
    paging = typeof paging !== "undefined" ? paging : true;
    searching = typeof searching !== "undefined" ? searching : true;

    var buttonCommon = {
        exportOptions: {
            columns: hideLastColumn ? ":not(:last-child)" : ""
        }
    };

    var tExport = "Export Actual Page";
    var tCopy = "Copy to Clipboard";
    var tPrint = "Print";
    if (window.culture.toLowerCase() === "pt-br") {
        tExport = "Exportar Página Atual";
        tCopy = "Área de Transferência";
        tPrint = "Imprimir";
    }
    tExport = "<i class='fa fa-download'></i>";
    var buttons = new $.fn.dataTable.Buttons(oTable, {
        buttons: [
            {
                extend: "collection",
                text: tExport,
                columns: [0, 1],
                buttons: [
                    $.extend(true, {}, buttonCommon, {
                        extend: "copyHtml5",
                        text: tCopy
                    }),
                    $.extend(true, {}, buttonCommon, {
                        extend: "excelHtml5",
                        title: title
                    }),
                    $.extend(true, {}, buttonCommon, {
                        extend: "csvHtml5",
                        title: title
                    }),
                    $.extend(true, {}, buttonCommon, {
                        extend: "pdfHtml5",
                        orientation: 'landscape',
                        title: title
                    }),
                    //$.extend(true, {}, buttonCommon, {
                    //    extend: "print",
                    //    text: tPrint,
                    //    title: title
                    //})
                ]
            }
        ]
    });
    //$("<div class='row'><div class='col-sm-12'><div id='" + tableName.replace("#", "") + "_buttons'></div></div></div>").insertBefore(tableName + "_wrapper");
    //$(tableName + "_buttons").html(oTable.buttons(0, null).containers());
    if (paging)
        oTable.buttons(0, null).containers().insertBefore(tableName + "_length");
    else if (searching)
        oTable.buttons(0, null).containers().insertBefore(tableName + "_filter");
    else
        oTable.buttons(0, null).containers().insertBefore(tableName + "_wrapper");
}
