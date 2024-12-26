using Microsoft.AspNetCore.Mvc;

namespace DemoCICD.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult TestAPI()
    {
        return Ok("Thành công");
    }
}
