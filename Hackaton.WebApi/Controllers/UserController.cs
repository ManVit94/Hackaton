using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Hackaton.Business.Interfaces;

namespace Hackaton.WebApi.Controllers
{
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}/videos")]
        public async Task<IActionResult> Videos(int userId, string priority)
        {
            var videos = await _userService.GetVideosAsync(userId, priority);

            return Ok(videos);
        }

        [HttpGet("{userId}/videos/{videoId}")]
        public async Task<IActionResult> Paths(int userId, int videoId, string priority)
        {
            var paths = await _userService.GetPathsToVideAsync(userId, videoId, priority);

            return Ok(paths);
        }
    }
}
