using Microsoft.AspNetCore.Mvc;

namespace Ubam.Evolution.Presentation.Controllers;

public class ErrorController : Controller
{
    [Route("/Error/NotFound")]
    public new IActionResult NotFound()
    {
        return View();
    }
}