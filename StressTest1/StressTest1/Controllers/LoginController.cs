using Microsoft.AspNetCore.Mvc;

using StressTest1.Models;

using System.Collections.Concurrent;

namespace StressTest1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private static readonly ConcurrentDictionary<string, string> Session = new();
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult PostLogin(LoginReq req)
        {
            try
            {
                _logger.LogInformation($"Post: {req.Id}");
                string sessionId = Guid.NewGuid().ToString();
                LoginRes res = new LoginRes()
                {
                    Id = req.Id,
                    SessionId = sessionId,
                };
                Session.TryAdd(sessionId, req.Id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{sessionId}")]
        public IActionResult Get(string sessionId)
        {
            _logger.LogInformation($"Get: {sessionId}");
            if (Session.TryGetValue(sessionId, out var id) == false)
                return NotFound();
            return Ok(id);
        }
    }
}
