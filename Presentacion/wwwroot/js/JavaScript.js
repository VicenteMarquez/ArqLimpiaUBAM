/*
document.addEventListener("contextmenu", (event) => {
    event.preventDefault();
});


document.addEventListener("keydown", (event) => {
    bloquearTeclas(event);
});

document.addEventListener("keyup", (event) => {
    bloquearTeclas(event);
});

function bloquearTeclas(event) {

    if (event.key === "F12") {
        event.preventDefault();
    }

    if (event.ctrlKey && event.shiftKey && event.key === "I") {
        event.preventDefault();
    }

    if (event.ctrlKey && event.shiftKey && event.key === "J") {
        event.preventDefault();
    }

    if (event.ctrlKey && event.key === "u") {
        event.preventDefault();
    }
}*/

$('#LoginForm').submit(function (event) {
    event.preventDefault();

    var formData = {
        Usuario_Nombre: $('#Usuario_Nombre').val(),
        Usuario_Contrasena: $('#Usuario_Contrasena').val()
    };

    $.ajax({
        url: 'https://localhost:7150/login',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(formData),
        success: function (response) {
            if (response.success) {
                window.location.href = response.redirectUrl;
            } else {
                var messageContainer = document.getElementById("message-container");
                messageContainer.hidden = false;
                messageContainer.innerHTML = "<center><p>Error al iniciar sesión, verifique sus credenciales.</p ></center > ";
                $('#LoginForm')[0].reset(); 
            }
        }
    });
});

$('#registroUsuarioForm').submit(function (event) {
    event.preventDefault();
    var formData = {
        Persona_Nombre: $('#Persona_Nombre').val(),
        Persona_ApellidoPaterno: $('#Persona_ApellidoPaterno').val(),
        Persona_ApellidoMaterno: $('#Persona_ApellidoMaterno').val(),
        Persona_FechaNacimiento: $('#Persona_FechaNacimiento').val(),
        Contacto_Correo: $('#Contacto_Correo').val(),
        Contacto_TelefonoCasa: $('#Contacto_TelefonoCasa').val(),
        Contacto_TelefonoPersonal: $('#Contacto_TelefonoPersonal').val(),
        Usuario_Nombre: $('#Usuario_Nombre').val(),
        Usuario_Contrasena: $('#Usuario_Contrasena').val(),
        Usuario_Rol: $('#Usuario_Rol').val(),
        Usuario_Activo: 1
    };

    $.ajax({
        url: 'https://localhost:7150/registercompleteuser',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(formData),
        success: function (response) {
            if (response.success) {
              
                window.location.href = response.redirectUrl;
            } else {
                var messageContainer = document.getElementById("message-container");
                messageContainer.hidden = false; 
                messageContainer.innerHTML = "<center><p>Error de registro de usuario.</p></center>";
                $('#registroUsuarioForm')[0].reset();
            }
        }
    });
});

