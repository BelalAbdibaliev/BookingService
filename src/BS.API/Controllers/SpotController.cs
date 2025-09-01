using BS.Application.Interfaces;
using BS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BS.API.Controllers;

[Controller]
[Route("api/spot")]
public class SpotController: Controller
{
    private readonly ISpotService _spotService;

    public SpotController(ISpotService spotService)
    {
        _spotService = spotService;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllSpots()
    {
        return Ok(await _spotService.GetAllSpots());
    }
    
    [HttpGet("get")]
    public async Task<IActionResult> GetSpotById(int id)
    {
        var  spot = await _spotService.GetSpotById(id);
        if (spot == null)
            return NotFound();
        
        return Ok(spot);
    }
}