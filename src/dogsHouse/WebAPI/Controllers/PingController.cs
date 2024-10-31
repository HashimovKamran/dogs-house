using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("GlobalRateLimit")]
public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Dogshouseservice.Version1.0.1");
    }
}
