using System.Diagnostics;
using System.Security.Claims;
using Application.Common.Models;
using Application.Contracts;
using Application.DTOs;
using Application.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Ubam.Evolution.Domain.Entities;
using Ubam.Evolution.Domain.Exceptions;
using Ubam.Evolution.Domain.Interfaces;

namespace Ubam.Evolution.Presentation.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IMemoryCache _cache;
    private readonly IPasswordService _passwordService;
    private readonly PersonMapper _personMapper;
    private readonly IPersonRepository _personRepository;
    private readonly IRolRepository _roleRepository;
    private readonly UserMapper _userMapper;
    private readonly IUserRepository _userRepository;
    private readonly UserRoleMapper _userRoleMapper;
    private readonly IUserRoleRepository _userRoleRepository;

    public HomeController(IPersonRepository personRepository,
        IUserRepository userRepository, IPasswordService passwordService, IRolRepository roleRepository,
        IUserRoleRepository userRoleRepository, PersonMapper personMapper, UserMapper userMapper,
        UserRoleMapper userRoleMapper, IMemoryCache cache)
    {
        _personRepository = personRepository;
        _userRepository = userRepository;
        _passwordService = passwordService;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _personMapper = personMapper;
        _userMapper = userMapper;
        _userRoleMapper = userRoleMapper;
        _cache = cache;
    }

    public IActionResult Index()
    {
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;

        var model = new HomeViewModel
        {
            UserName = userName,
            Role = role
        };

        return View(model);
    }

    [Authorize(Roles = "Administrador")]
    [Route("/Docentes")]
    public async Task<IActionResult?> Docente()
    {
        try
        {
            var cachekey = "lista_docentes";
            if (!_cache.TryGetValue(cachekey, out List<Persona> docentes))
            {
                docentes = await _personRepository.GetDocentesAsync();

                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };
                _cache.Set(cachekey, docentes, cacheOptions);
            }

            return View(docentes);
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
    }

    [Authorize(Roles = "Docente, Administrador")]
    [Route("/Alumnos")]
    public async Task<IActionResult> Alumno()
    {
        try
        {
            var cachekey = "lista_alumnos";
            if (!_cache.TryGetValue(cachekey, out List<Persona> alumnos))
            {
                alumnos = await _personRepository.GetAlumnosAsync();

                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };
                _cache.Set(cachekey, alumnos, cacheOptions);
            }

            return View(alumnos);
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
    }

    [Authorize(Roles = "Administrador")]
    [Route("/agregar-usuario")]
    public IActionResult AddUser()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    [Route("/agregar-usuario")]
    public async Task<IActionResult> AddUser(UserRequestDto model)
    {
        if (!ModelState.IsValid) return View(model);

        try
        {
            // Agregar persona
            var person = _personMapper.ToEntity(model);
            await _personRepository.AddAsync(person);

            // Agregar Usuario
            var personResponse = await _personRepository.GetByCurpAsync(model.Curp);
            var hashedPassword = _passwordService.HashPassword(model.UserPassword);
            var user = _userMapper.ToEntity(model, hashedPassword, personResponse.Id_Persona);
            await _userRepository.AddAsync(user);

            // Asignar rol a usuario
            var roleId = await _roleRepository.GetRolIdByRolNameAsync(model.Role);
            var userRole = _userRoleMapper.ToEntity(user.Id_Usuario, roleId);
            await _userRoleRepository.AddAsync(userRole);

            return RedirectToAction("Index");
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}