(() => {
    'use strict'
    const forms = document.querySelectorAll('.needs-validation')

    Array.from(forms).forEach(form => {
        form.addEventListener('submit', event => {
            if (!form.checkValidity()) {
                event.preventDefault()
                event.stopPropagation()
            }
            form.classList.add('was-validated')
        }, false)
    })
})()

function togglePassword() {
    const passwordInput = document.getElementById('userPassword');
    const toggleIcon = document.querySelector('.password-toggle');

    if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
        toggleIcon.classList.replace('bi-eye-slash', 'bi-eye');
    } else {
        passwordInput.type = 'password';
        toggleIcon.classList.replace('bi-eye', 'bi-eye-slash');
    }
}

document.getElementById('userPassword').addEventListener('input', function () {
    const password = this.value;
    const progressBar = document.querySelector('.progress-bar');
    const strengthText = document.querySelector('.password-strength-text');

    let strength = 0;

    if (password.length >= 8) strength += 25;
    if (password.match(/[A-Z]/)) strength += 25;
    if (password.match(/[0-9]/)) strength += 25;
    if (password.match(/[^A-Za-z0-9]/)) strength += 25;

    progressBar.style.width = strength + '%';

    if (strength <= 25) {
        progressBar.className = 'progress-bar bg-danger';
        strengthText.textContent = 'Débil';
    } else if (strength <= 50) {
        progressBar.className = 'progress-bar bg-warning';
        strengthText.textContent = 'Regular';
    } else if (strength <= 75) {
        progressBar.className = 'progress-bar bg-info';
        strengthText.textContent = 'Buena';
    } else {
        progressBar.className = 'progress-bar bg-success';
        strengthText.textContent = 'Fuerte';
    }
});