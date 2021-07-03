using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Hackaton.Business.Interfaces;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;
using Hackaton.DataContracts;
using System.Diagnostics;

namespace Hackaton.WebApi.Controllers
{
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEventPublisher _eventPublisher;

        public UserController(IUserService userService, IEventPublisher eventPublisher)
        {
            _userService = userService;
            _eventPublisher = eventPublisher;
        }

        [HttpGet("{userId}/videos")]
        public async Task<IActionResult> Videos(int userId, string priority)
        {
            var sw = new Stopwatch();
            sw.Start();

            var videos = await _userService.GetVideosAsync(userId, priority);

            sw.Stop();

            _eventPublisher.Publish(new DataContracts.Messages.RequestCalledEvent
            {
                ElapsedTime = sw.ElapsedMilliseconds,
                ItemsCount = videos.Count,
                Url = HttpContext.Request.Path
            });

            return Ok(videos);
        }

        [HttpGet("{userId}/videos/{videoId}")]
        public async Task<IActionResult> Paths(int userId, int videoId, string priority)
        {
            var sw = new Stopwatch();
            sw.Start();

            var paths = await _userService.GetPathsToVideAsync(userId, videoId, priority);

            sw.Stop();

            _eventPublisher.Publish(new DataContracts.Messages.RequestCalledEvent
            {
                ElapsedTime = sw.ElapsedMilliseconds,
                ItemsCount = paths.Count,
                Url = HttpContext.Request.Path
            });

            return Ok(paths);
        }
    }
}
