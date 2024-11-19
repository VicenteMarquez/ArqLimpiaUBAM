$('#form-catalog').find(':input[data-date="true"]').each(function () {
    CrearDatePiker($(this));
});

$('#btn-agregar-catalog').click(function () {
    IniciarFormulario("form-catalog");
    $('.boton-para-guardar').attr('id', 'btn-guardar');
    $('#modal-catalog').modal('show');
});

$('body').on('click', '#btn-guardar', function () {
    var formValid = $('#form-catalog').formvalidate();
    if (formValid) {
        var lstElement = ArreglarCatalogo('form-catalog');
        var info = {
            lstElement
        };
        GuardarCatalogo(info);
    }
});

$('body').on('click', '#btn-actualizar', function () {
    var formValid = $('#form-catalog').formvalidate();
    if (formValid) {
        var lstElement = ArreglarCatalogo('form-catalog');
        var info = {
            lstElement
        };
        EditarCatalogo(info);
    }
});

$('tbody').on('click', '#btn-detalles', function () {
    var currentRow = $(this).closest("tr");
    var id = currentRow.find("td:eq(1)").text().replace(/\s/g, '');
    VerDetalle(id);
});

$('tbody').on('click', '#btn-editar', function () {
    var currentRow = $(this).closest("tr");
    var id = currentRow.find("td:eq(1)").text().replace(/\s/g, '');
    VerDetalle(id);
});

$('tbody').on('click', '#btn-eliminar', function () {
    var currentRow = $(this).closest("tr");
    var id = currentRow.find("td:eq(1)").text().replace(/\s/g, '');
    
    swal({
        title: "Eliminar registro",
        text: "El registro se eliminará de forma permanente y no podrá recuperarse después.",
        type: "warning",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Aceptar",
        cancelButtonText: "Cancelar"
    }, function (isConfirm) {
        if (isConfirm) {
            //alert('TODO: confirmar y eliminar registro: ' + id);
            EliminarRegistro(id);
        }
    });
});

function VerDetalle(id) {
    var dataCatalog = ConsultarDetalle(id);

    $.each(dataCatalog.Elements.Element, function (index, data) {
        $("form#form-catalog input").each(function () {
            if (this.name == data.FieldName) {
                if (data.Type == 'DATE')
                    $(this).val(moment(data.Value).format("YYYY-MM-DD h:mm a"));
                else
                    $(this).val(data.Value);
                
                if (this.type == "checkbox" && data.Value == "True")
                    $(this).parent('[class*="icheckbox"]').addClass("checked");
                else
                    $(this).parent('[class*="icheckbox"]').removeClass("checked");
            }
            //TODO: manejar el caso de los elementos radio cuando se requiera
        });
        $("form#form-catalog select").each(function () {
            if (this.name == data.FieldName)
                $(this).val(data.Value);
        });
        $("textarea").each(function () {
            if (this.name == data.FieldName)
                $(this).val(data.Value);
        });
    });

    $('#id-data').val(id);
    $('.boton-para-guardar').attr('id', 'btn-actualizar');
    $('#modal-catalog').modal('show');
}

function ConsultarDetalle(id) {
    var result;
    $.ajax({
        url: obtenBase() + "Catalog/ObtenerRegistro",
        type: "POST",
        data: { unityId: $('#unity-catalog').val(), catname: $('#name-catalog').val(), catid: id },
        content: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (Data) {
            result = Data;
        },
        error: function (xhr, mjs, data) {
            debugger;
            alert(xhr + mjs + data);
        }
    });
    return result;
}

function CrearDatePiker(inputCat) {
    inputCat.datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true,
        format: 'yyyy-mm-dd'
    });
}

function ArreglarCatalogo(form) {
    var data = new Array();
    $("form#" + form + " input").each(function () {
        var htmlelement = {};
        htmlelement["elemento"] = this.name;
        htmlelement["tipo"] = this.type;
        if ((this.type == "radio" && this.checked) || this.type == "text" || this.type == "hidden") {
            htmlelement["valor"] = $(this).val();
            data.push(htmlelement);
        }
        if (this.type == "checkbox") {
            if (this.id.startsWith("BO")) {
                htmlelement["valor"] = $(this).parent('[class*="icheckbox"]').hasClass("checked");
                data.push(htmlelement);
            }
            else if (this.id.toString().startsWith("CB") && this.checked) {
                htmlelement["valor"] = $(this).val();
                data.push(htmlelement);
            }
        }
    });
    $("form#" + form + " select").each(function () {
        var htmlelement = {};
        htmlelement["elemento"] = this.name;
        htmlelement["tipo"] = this.type;
        htmlelement["valor"] = $(this).val();
        data.push(htmlelement);
    });
    $("textarea").each(function () {
        var htmlelement = {};
        htmlelement["elemento"] = this.name;
        htmlelement["tipo"] = this.type;
        htmlelement["valor"] = $(this).val();
        data.push(htmlelement);
    });
    return data;
}

function GuardarCatalogo(datos) {
    $.ajax({
        url: obtenBase() + "Catalog/GuardarInfo",
        type: "POST",
        dataType: "json",
        data: { unityId: $('#unity-catalog').val(), catname: $('#name-catalog').val(), datos: datos },
        success: function (dataResponse) {
            if (ShowProccessResponse(dataResponse)) {
                $('#modal-catalog').modal("hide");
                setTimeout(function () {
                    location.href = obtenBase() + "Catalog/Index?unityId=" + $('#unity-catalog').val() + "&catname=" + $('#name-catalog').val();
                }, 1200)
            }
        },
        error: function (xhr, mjs, data) {
            debugger;
            alert(xhr + mjs + data);
        }
    });
}

function EditarCatalogo(datos) {
    $.ajax({
        url: obtenBase() + "Catalog/EditarInfo",
        type: "POST",
        dataType: "json",
        data: { unityId: $('#unity-catalog').val(), catname: $('#name-catalog').val(), datos: datos },
        success: function (dataResponse) {
            if (ShowProccessResponse(dataResponse)) {
                $('#modal-catalog').modal("hide");
                setTimeout(function () {
                    location.href = obtenBase() + "Catalog/Index?unityId=" + $('#unity-catalog').val() + "&catname=" + $('#name-catalog').val();
                }, 1200)
            }
        },
        error: function (xhr, mjs, data) {
            debugger;
            alert(xhr + mjs + data);
        }
    });
}

function EliminarRegistro(id) {
    $.ajax({
        url: obtenBase() + "Catalog/EliminarInfo",
        type: "POST",
        dataType: "json",
        data: { unityId: $('#unity-catalog').val(), catname: $('#name-catalog').val(), catid: id },
        success: function (dataResponse) {
            if (ShowProccessResponse(dataResponse)) {
                $('#modal-catalog').modal("hide");
                setTimeout(function () {
                    location.href = obtenBase() + "Catalog/Index?unityId=" + $('#unity-catalog').val() + "&catname=" + $('#name-catalog').val();
                }, 1200)
            }
        },
        error: function (xhr, mjs, data) {
            debugger;
            alert(xhr + mjs + data);
        }
    });
}