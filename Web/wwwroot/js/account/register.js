async function register() {
  const username = $('#username').val();
  const password = $('#password').val();
  const confirmPassword = $('#confirmPassword').val();

  if (password !== confirmPassword) {
    alert('Las contraseñas no coinciden.');
    return;
  }

  const response = await fetch('/api/auth/register', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ username, password, role: 'User' })
  });

  if (response.ok) {
    alert('Registro exitoso. Por favor, inicia sesión.');
    window.location.href = "/auth/login";
  } else {
    alert('Error en el registro. Intente nuevamente.');
  }
}
