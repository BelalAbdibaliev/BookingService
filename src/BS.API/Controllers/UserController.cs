using BS.API.Dto;
using BS.Application.Interfaces;
using BS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BS.API.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class UserController: Controller
{
    private IJwtTokenGenerator jwtTokenGenerator;
    
    public UserController(IJwtTokenGenerator  jwtTokenGenerator)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
    }
    
    [HttpGet("login")]
    public async Task<IActionResult> LogIn()
    {
        var token = jwtTokenGenerator.GenerateToken(new User()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "xddd",
            Email = "xdddddddd@gmail.com"
        }, new List<string>()
        {
            "USER",
            "ADMIN"
        });
        return Ok(token);
    }

    [Authorize]
    [HttpGet("logout")]
    public async Task<IActionResult> LogOut()
    {
        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        return Ok();
    }
}