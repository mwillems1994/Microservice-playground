using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pitstop.Infrastructure.Messaging;
using Playground.Nl.CustomerManagementAPI.Services.Events;
using Serilog;

namespace Playground.Nl.CustomerManagementAPI.Nl.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("/api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IMessagePublisher _messagePublisher;

        public TestController(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            var e = new CustomerRegistered();
            await _messagePublisher.PublishMessageAsync(e.MessageType, e , "");
            Log.Information("Test log");

            return Ok(new
            {
                Foo = "Foo",
                Bar = "Bar"
            });
        }
    }
}