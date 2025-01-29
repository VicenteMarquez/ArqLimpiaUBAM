using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{

    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        [HttpGet("index")]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var ruta = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Admin", "Index.html");
                var contenido = System.IO.File.ReadAllText(ruta);
                return Content(contenido, "text/html");
            }
            return Ok(new { success = false, redirectUrl = Url.Action("InicioSesion", "Autenticacion") });
        }

        [HttpGet("Registrodato")]
        public IActionResult RegistroDato()
        {
            var ruta = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Admin", "RegistroDato.html");
            var contenido = System.IO.File.ReadAllText(ruta);
            return Content(contenido, "text/html");
        }
    }
}
