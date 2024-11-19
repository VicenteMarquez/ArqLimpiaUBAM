function obtenBase() {
    //Produccion 
	//var base = "https://www.ubam.intranet.edu/"
    //Desarrollo
    var base = "http://localhost:54385/";
    //QA
    //var base ="https://194.168.1.24:494/"
    return base;
}
function GetQueryString(name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    if (results != null) {
        return results[1] || 0;
    }
    else {
        return 0;
    }
}
function mostrarModalMensajes(dto) {
    var completado;
    var showTime = 3000;
    $("#ModalMSG .modal-header").removeClass("bg-success");
    $("#ModalMSG .modal-header").removeClass("bg-warning");
    if (dto.IdCodigo <= 250) {
        $("#ModalHeaderMSG").html('<i class="fas fa-exclamation-triangle mr-2"></i>Error');
        $("#ModalMSG .modal-header").addClass("bg-warning");
        $("#ModalHeaderMSG").css("font-size", "20px");
        $("#ModalMessage").text(dto.Mensaje);
        $("#ModalMessage").css("font-size", "15px");
        setTimeout(function () { $('#ModalMSG').modal('hide'); }, showTime);
        completado = false;
    }
    if (dto.IdCodigo >= 251) {
        $("#ModalHeaderMSG").text("Completado");
        $("#ModalHeaderMSG").addClass("text-white");
        $("#ModalHeaderMSG").css("font-size", "20px");
        $("#ModalMSG .modal-header").addClass("bg-success");
        if (dto.MensajeFormat !== null) {
            showTime = 6000;
            $("#ModalMessage").html(dto.Mensaje + "<br><b>" + dto.MensajeFormat + "</b>");
        }
        else {
            $("#ModalMessage").text(dto.Mensaje);
        }
        $("#ModalMessage").css("font-size", "15px");
        setTimeout(function () { $('#ModalMSG').modal('hide'); }, showTime);
        completado = true;
    }
    $("#ModalMSG").modal("show");
    return completado;
}
function ToJavaScriptDate(value) {
    try {
        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(value);
        var result = new Date(parseFloat(results[1]));
        //var result = new Date(dt.getFullYear(), dt.getMonth()+1, dt.getDate());
        var mes = result.getMonth() + 1;
        var dia = result.getDate();
        if (mes < 10) {
            if (mes < 1) {
                mes = mes + 1;
            }
            var mes = "0" + mes;
        }
        if (dia < 10) {

            dia = "0" + dia
        }
        return dia + "-" + mes + "-" + result.getFullYear();
    }
    catch (exception) {
        return value;
    }
}

function IniciarFormulario(form) {
    $("form#" + form + " :input[type='text']").each(function () {
        $(this).removeAttr("readonly");
        $(this).removeAttr("disabled");
        $(this).val("");
    });
    $("form#" + form + " :input[type='date']").each(function () {
        $(this).removeAttr("readonly");
        $(this).removeAttr("disabled");
        $(this).val("");
    });
    $("form#" + form + " :input[type='number']").each(function () {
        $(this).removeAttr("readonly");
        $(this).removeAttr("disabled");
        $(this).val("");
    });
    $("form#" + form + " select").each(function () {
        $(this).removeAttr("readonly");
        $(this).removeAttr("disabled");
        $(this).val("");
    });
    $("form#" + form + " :input[type='checkbox']").each(function () {
        $(this).parent('[class*="icheckbox"]').removeClass("checked");
    });
    $("textarea").each(function () {
        $(this).val("");
    });
}

function ShowProccessResponse(resp) {
    var completado;

    if (resp.IdCodigo == 109) {
        window.location = obtenBase() + "Account/Login";
    }
    else if (resp.IdCodigo <= 250) {
        swal({
            title: "Ups.",
            text: resp.Mensaje,
            type: "error"
        });
        completado = false;
    }
    else {
        if (resp.MensajeFormat !== null) {
            swal({
                title: "Genial!",
                text: resp.Mensaje + '\n\n' + resp.MensajeFormat,
                type: "success"
            });
        }
        else {
            swal({
                title: "Genial!",
                text: resp.Mensaje,
                type: "success",
                timer: 2000,
                showConfirmButton: false
            });
        }
            completado = true;
    }
    return completado;
}

function ShowProccessNotification(resp) {
    var completado;
    if (resp.IdCodigo == 109) {
        window.location = obtenBase() + "Account/Login";
    }
    else if (resp.IdCodigo <= 250) {
        ShowNotification("error", "No se pudo completar la acción.", resp.Mensaje, false);
        completado = false;
    }
    else {
        ShowNotification("success", "Acción completada.", resp.Mensaje, true);
        completado = true;
    }
    return completado;
}

// shortCutFunction único parámetro obligatorio, todos los opcionales se establecen en su valor por default.
// shortCutFunction could be: success, info, warning or error
// position could be: toast-top-right, toast-bottom-right, toast-bottom-left, toast-top-left, toast-top-full-width, toast-bottom-full-width, toast-top-center, toast-bottom-center
function ShowNotification(shortCutFunction, title, msg, temporal, position, element) {
    var $toastlast;
    var getMessage = function () {
        var msg = 'Hola! bienvenido a ERP-UBAM, Esto es un ejemplo de notificación.';
        return msg;
    };
    toastr.options = {
        closeButton: true,
        debug: false,
        progressBar: true, 
        positionClass: position,
        showDuration: temporal ? 400 : 0,
        hideDuration: temporal ? 1000 : 0,
        timeOut: temporal ? 5000 : 0,
        extendedTimeOut: temporal ? 1000 : 0,
        onclick: null
    };
    toastr.options.onclick = function () {
        if (element != null)
            $(element).focus();
    };
    if (!msg) {
        msg = getMessage();
    }
    var $toast = toastr[shortCutFunction](msg, title);
    $toastlast = $toast;
}

function GetUnity() {
    var result;
    $.ajax({
        url: obtenBase() + "Home/GetUnity",
        type: "GET",
        content: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (Data) {
            result = Data;
        },
        error: function (xhr, status, error) {
            var errorMessage = xhr.status + ': ' + xhr.statusText
            console.log(this);
        }
    });
    return result;
}

$(document).ready(function () {

    var unity = GetUnity();
    $("#unity-id").val(unity);

    switch (unity) {
        case 0:
            $("body").removeClass("skin-1");
            $("body").removeClass("skin-2");
			$("body").removeClass("skin-3");
			$("body").removeClass("skin-4");
			$("body").removeClass("skin-5");
            break;
        case 1:
			$("body").removeClass("skin-1");
			$("body").removeClass("skin-2");
			$("body").removeClass("skin-3");
			$("body").addClass("skin-4");
			$("body").removeClass("skin-5");
            break;
        case 2:
            $("body").removeClass("skin-1");
            $("body").removeClass("skin-2");
			$("body").addClass("skin-3");
			$("body").removeClass("skin-4");
			$("body").removeClass("skin-5");
            break;
        case 3:
            $("body").removeClass("skin-1");
			$("body").addClass("skin-2");
			$("body").removeClass("skin-3");
			$("body").removeClass("skin-4");
			$("body").removeClass("skin-5");
            break;
        case 4:
            $("body").removeClass("skin-1");
            $("body").removeClass("skin-2");
            $("body").removeClass("skin-3");
            $("body").removeClass("skin-4");
            $("body").removeClass("skin-5");
            $("body").addClass("skin-6");
            break;
		case 5:
			$("body").removeClass("skin-1");
			$("body").removeClass("skin-2");
			$("body").removeClass("skin-3");
			$("body").removeClass("skin-4");
			$("body").addClass("skin-5");
			break;
		case 6:
			$("body").addClass("skin-1");
			$("body").removeClass("skin-2");
			$("body").removeClass("skin-3");
			$("body").removeClass("skin-4");
			$("body").removeClass("skin-5");
			break;
		default:
			$("body").removeClass("skin-1");
			$("body").addClass("skin-2");
			$("body").removeClass("skin-3");
			$("body").removeClass("skin-4");
			$("body").removeClass("skin-5");
            break;
    }

    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue',
    });

    // set boxedlayout by default
    if (localStorageSupport) {
        localStorage.setItem("fixednavbar", 'off');
    }

    if (localStorageSupport) {
        localStorage.setItem("fixednavbar2", 'off');
    }

    if (localStorageSupport) {
        localStorage.setItem("fixedfooter", 'off');
    }

    if (localStorageSupport) {
        localStorage.setItem("boxedlayout", 'on');
    }

});

function FileExists(urlToFile) {
    var xhr = new XMLHttpRequest();
    xhr.open('HEAD', urlToFile, false);
    xhr.send();

    if (xhr.status == "404" || xhr.status == "500") {
        return false;
    } else {
        return true;
    }
}