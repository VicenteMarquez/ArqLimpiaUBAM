using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [Authorize(Roles ="empleado")]
    [ApiController]
    [Route("[controller]")]
    public class EmpleadoController : Controller
    {
        [HttpGet("index")]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var ruta = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Empleado", "Index.html");
                var contenido = System.IO.File.ReadAllText(ruta);
                return Content(contenido, "text/html");
            }
            return Ok(new { success = false, redirectUrl = Url.Action("InicioSesion", "Autenticacion") });
        }


    }
}
