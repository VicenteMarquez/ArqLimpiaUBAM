
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [Authorize(Roles = "cliente")]
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : Controller
    {
        [HttpGet("index")]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var ruta = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Cliente", "Index.html");
                var contenido = System.IO.File.ReadAllText(ruta);
                return Content(contenido, "text/html");
            }
            return Ok(new { success = false, redirectUrl = Url.Action("InicioSesion", "Autenticacion") });
        }

    }
}
