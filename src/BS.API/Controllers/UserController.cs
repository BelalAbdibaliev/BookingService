using BS.Application.Dto;
using BS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BS.API.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var token = await _userService.LoginAsync(dto);
        if (token != null)
            return Ok(token);

        return Unauthorized("Неверные данные или email не подтвержден.");
    }

    [Authorize]
    [HttpGet("logout")]
    public async Task<IActionResult> LogOut()
    {
        await _userService.LogoutAsync();
        return Ok("Вы вышли из системы.");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
    
        var result = await _userService.RegisterAsync(dto);

        if (result != null)
            return Ok(new { message = result });
    
        return BadRequest("Ошибка регистрации");
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
    {
        var result = await _userService.ConfirmEmailAsync(userId, token);
        if (result)
            return Ok("Email подтвержден. Теперь вы можете войти.");
        return BadRequest("Невозможно подтвердить email.");
    }

    [HttpPost("resend-confirmation")]
    public async Task<IActionResult> ResendConfirmationEmail([FromBody] string email)
    {
        var result = await _userService.ResendConfirmationEmailAsync(email);
        if (result)
            return Ok("Письмо с подтверждением отправлено повторно.");
        return BadRequest("Невозможно отправить письмо.");
    }
}
