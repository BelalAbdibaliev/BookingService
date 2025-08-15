using AutoMapper;
using BS.Application.Dto;
using BS.Application.Interfaces;
using BS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Web;
using Microsoft.Extensions.Logging;

namespace BS.Application.Services;

public class UserService : IUserService
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IEmailSender _emailSender;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IAuthService authService,
        IMapper mapper,
        IJwtTokenGenerator jwtTokenGenerator,
        IEmailSender emailSender,
        UserManager<User> userManager,
        ILogger<UserService> logger)
    {
        _authService = authService;
        _mapper = mapper;
        _jwtTokenGenerator = jwtTokenGenerator;
        _emailSender = emailSender;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<string?> LoginAsync(LoginDto userDto)
    {
        var user = await _authService.LoginAsync(userDto.Email, userDto.Password);

        if (user == null) return null;
        if (!await _userManager.IsEmailConfirmedAsync(user)) return null;

        return await _jwtTokenGenerator.GenerateTokenAsync(user);
    }

    public async Task<string?> RegisterAsync(RegisterUserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        var result = await _authService.RegisterAsync(user, userDto.Password);
        if (result == null)
        {
            _logger.LogError("User registration failed");
            return null;
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = HttpUtility.UrlEncode(token);
        var confirmationLink = $"https://yourfrontend.com/confirm-email?userId={user.Id}&token={encodedToken}";

        await _emailSender.SendEmailAsync(
            user.Email,
            "Подтверждение регистрации",
            $"<p>Здравствуйте, {user.UserName}!</p><p>Подтвердите регистрацию по <a href='{confirmationLink}'>ссылке</a>.</p>"
        );

        return "Пользователь зарегистрирован, подтвердите email";
    }


    public async Task LogoutAsync()
    {
        await _authService.LogoutAsync();
    }

    public async Task<bool> ConfirmEmailAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            _logger.LogError($"User with id {userId} not found.");
            return false;
        }

        var decodedToken = System.Web.HttpUtility.UrlDecode(token);

        
        var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
        if(!result.Succeeded) _logger.LogError($"{result.Errors.First().Description}: {result.Errors.First().Code}.");
        
        return result.Succeeded;
    }

    public async Task<bool> ResendConfirmationEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || user.EmailConfirmed) return false;

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = HttpUtility.UrlEncode(token);

        var confirmationLink = $"https://frontend.com/confirm-email?userId={user.Id}&token={encodedToken}";

        await _emailSender.SendEmailAsync(
            user.Email,
            "Подтверждение регистрации (повторно)",
            $"<p>Подтвердите регистрацию по <a href='{confirmationLink}'>ссылке</a>.</p>"
        );

        return true;
    }
}
