using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Playground.Nl.CustomerManagementAPI.Nl.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("/api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok(new
            {
                Foo = "Foo",
                Bar = "Bar"
            });
        }
    }
}