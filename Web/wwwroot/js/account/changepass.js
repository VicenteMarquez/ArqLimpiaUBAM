$("#BtnVerContraseñaActual")
    .mouseup(function () {
        $("#ContraseñaActual").attr("type", "password");
    })
    .mousedown(function () {
        $("#ContraseñaActual").attr("type", "text");
    });
$("#BtnVerContraseñaNueva")
    .mouseup(function () {
        $("#ContraseñaNueva").attr("type", "password");
    })
    .mousedown(function () {
        $("#ContraseñaNueva").attr("type", "text");
    });
$("#BtnVerContraseñaNueva2")
    .mouseup(function () {
        $("#ContraseñaNueva2").attr("type", "password");
    })
    .mousedown(function () {
        $("#ContraseñaNueva2").attr("type", "text");
    });

function CambiarContraseña() {
    var contraseñaNueva = $("#ContraseñaNueva").val();
    var repetirContraseña = $("#ContraseñaNueva2").val();
    if (contraseñaNueva == repetirContraseña) {
        //if (validarForm().form.valid()) {
            $.ajax({
                async: false,
                url: obtenBase() + "/Account/CambiarPassword",
                type: "POST",
                data: { passOld: $("#ContraseñaActual").val(), passNew: $("#ContraseñaNueva").val() },
                success: function (responseData) {
					setTimeout(function () {
						window.location.href = obtenBase();
					}, 2000)
                },
                error: function (xhr, estatus, code) {
                }
            });
        //}
    }
    else {
        var respuesta = { Mensaje: "Las contraseñas nuevas no coinciden.", IdCodigo: 104 }
        ShowProccessResponse(respuesta);
    }

}