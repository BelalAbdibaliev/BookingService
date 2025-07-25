using BS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BS.API.Controllers;

[Controller]
[Route("api/resources")]
public class ResourceController: Controller
{
    private readonly IResourceService _resourceService;

    public ResourceController(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get([FromQuery]int id)
    {
        var resource = await _resourceService.FindAsync(id);
        
        if(resource != null)
            return Ok(resource);
        
        return NotFound();
    }

    [HttpGet("getalll")]
    public async Task<IActionResult> GetAll()
    {
        var resources = await _resourceService.GetAllAsync();
        if (resources != null)
            return Ok(resources);
        
        return NotFound();
    }
}