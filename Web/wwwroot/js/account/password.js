$(document).ready(function () {

});
function regresar() {
    location.href = obtenBase();
}
function validar() {    
    $.ajax({
        type: "POST",
        url: obtenBase() + "Account/Recuperar",
        data: { correo: $('#txt-correo').val() },
        success: function (dataResponse) {
            if (ShowProccessResponse(dataResponse)) {
                setTimeout(function () {
                    location.href = obtenBase();
                }, 1500)
            }
        }
    });
}
