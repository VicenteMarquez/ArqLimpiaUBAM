async function login() {
  const username = $('#username').val();
  const password = $('#password').val();

  const response = await fetch('/api/auth/login', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ username, password })
  });

  if (response.ok) {
    const data = await response.json();
    localStorage.setItem('token', data.token);
    window.location.href = "/home"; // Redirige a la página principal
  } else {
    alert('Usuario o contraseña incorrectos.');
  }
}
