using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
  [HttpGet]
  public IActionResult Get()
  {
    return Ok("Hello from Docker");
  }
}