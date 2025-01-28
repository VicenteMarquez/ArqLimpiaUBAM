$(document).ready(function () {
    console.log("hola")
    $('#loginForm').on('submit', function (event) {
        event.preventDefault();

        const username = $('#username').val();
        const password = $('#password').val();

        $.ajax({
            url: '/Auth/Login/',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({username, password}),
            success: function (response) {
                window.location.href = response.redirectUrl;
            },
            error: function (xhr) {
                const error = xhr.responseJSON?.error || "Error desconocido";
                $('#errorAlert').text(error).show();
            }
        });
    });
});