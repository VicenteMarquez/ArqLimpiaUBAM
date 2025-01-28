using Application.Contracts;
using Application.DTOs;
using Application.Mappers;
using Ubam.Evolution.Domain.Enum;
using Ubam.Evolution.Domain.Exceptions;
using Ubam.Evolution.Domain.Interfaces;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly ILoginRepository _loginRepository;
    private readonly IPasswordService _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;

    public AuthService(IUserRepository userRepository, IUserRoleRepository userRoleRepository,
        IPasswordService passwordHasher, IJwtService jwtService, ILoginRepository loginRepository)
    {
        _userRoleRepository = userRoleRepository;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _loginRepository = loginRepository;
    }

    public async Task<LoginResponseDto?> Authenticate(LoginRequestDto loginRequest)
    {
        var user = await _userRepository.GetUserByUsernameAsync(loginRequest.Username);
        var mapper = new LoginHistoryMapper();
        string description;
        HistoryLoginDto historyLoginDto;

        if (user == null || !_passwordHasher.VerifyPassword(user.Clave_Usuario, loginRequest.Password))
        {
            description = LoginException.GetMessage(LoginEnum.InvalidCredentials);
            historyLoginDto = mapper.ToDto(user.Id_Usuario, description);
            await _loginRepository.AddAsync(mapper.ToEntity(historyLoginDto));
            throw new ValidationException(ExceptionEnum.InvalidCredentials);
        }

        if (!user.Estatus_Usuario)
        {
            description = LoginException.GetMessage(LoginEnum.InactiveUser);
            historyLoginDto = mapper.ToDto(user.Id_Usuario, description);
            await _loginRepository.AddAsync(mapper.ToEntity(historyLoginDto));
            throw new ValidationException(ExceptionEnum.InactiveUser);
        }

        var roleName = await _userRoleRepository.GetRolAsync(user.Id_Usuario);
        if (roleName == null)
        {
            description = LoginException.GetMessage(LoginEnum.UnassignedRole);
            historyLoginDto = mapper.ToDto(user.Id_Usuario, description);
            await _loginRepository.AddAsync(mapper.ToEntity(historyLoginDto));
            throw new ValidationException(ExceptionEnum.RoleNotAssigned);
        }

        var userMapper = new UserMapper();
        var token = _jwtService.GenerateJwtToken(userMapper.ToDto(user.Id_Usuario, user.Nombre_Usuario, roleName));

        description = LoginException.GetMessage(LoginEnum.LoginSuccessful);
        historyLoginDto = mapper.ToDto(user.Id_Usuario, description);
        await _loginRepository.AddAsync(mapper.ToEntity(historyLoginDto));
        return new LoginResponseDto { Token = token, Username = user.Nombre_Usuario };
    }
}