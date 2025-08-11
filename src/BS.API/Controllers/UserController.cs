using BS.Application.Dto;
using BS.Application.Interfaces;
using BS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BS.API.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class UserController: Controller
{
    private IUserService _userService;
    
    public UserController(IUserService  userService)
    {
        _userService = userService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LoginDto dto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var token = await _userService.LoginAsync(dto);
        
        if(token != null)
            return Ok(token);
        
        return Unauthorized();
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
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var token = await _userService.RegisterAsync(dto);
        
        if(token != null)
            return Ok(token);
        
        return Unauthorized();
    }
}