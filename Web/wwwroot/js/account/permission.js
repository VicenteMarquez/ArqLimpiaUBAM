function AllowEdit(pantalla) {
	var result;
	$.ajax({
		type: "POST",
		url: obtenBase() + "Seguridad/Permiso/ValidarPermiso",
		data: { pantalla },
		async: false,
		success: function (dataResponse) {
			try {
				if (dataResponse.Mensaje == "OK") {
					result = dataResponse.Resultado;
					if (result == true) {
						$('.allow-edit').show();
					}
					else {
						$('.allow-edit').hide();
					}
				}
				else {
					ShowNotification('error', 'Error', dataResponse.Mensaje, false);
				}
			} catch (e) {
				alert(e);
			}
		}
	});
	return result;
}