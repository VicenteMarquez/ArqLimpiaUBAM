using Application.Contracts;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Ubam.Evolution.Domain.Exceptions;

namespace Ubam.Evolution.Presentation.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly IJwtService _jwtService;
    private readonly ICookiesService _cookiesService;

    public AuthController(IAuthService authService, IJwtService jwtService, ICookiesService cookiesService)
    {
        _authService = authService;
        _jwtService = jwtService;
        _cookiesService = cookiesService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Home");
        string htmlContent = System.IO.File.ReadAllText("C:\\Users\\sopor\\OneDrive\\Documentos\\Prácticas\\Zein\\NET\\WEB\\Ubam.Evolution.Presentation\\Views\\Auth\\Login.html");
        return Content(htmlContent, "text/html");
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        try
        {
            var loginResponse = await _authService.Authenticate(model);
            var authUser = _jwtService.ValidateToken(loginResponse.Token);

            await _cookiesService.SaveUserAsync(authUser, loginResponse.Token);
            return Ok(new { redirectUrl = Url.Action("Index", "Home") });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _cookiesService.ClearUserAsync();
        return RedirectToAction(nameof(Login));
    }
}