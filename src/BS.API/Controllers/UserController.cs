using Microsoft.AspNetCore.Mvc;

namespace BS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: Controller
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
}