using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Hackaton.WebApi.Controllers
{
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IMongoDatabase db;

        public UserController(IMongoDatabase db)
        {
            this.db = db;
        }

        [HttpGet("{userId}/videos")]
        public IActionResult Videos(int userId, string priority = "desc")
        {
            var arrayResultDesc = new[]
            {
                new { Video = "Video 5", Priority = "high" },
                new { Video = "Video 6", Priority = "high" },
                new { Video = "Video 7", Priority = "high" },
                new { Video = "Video 8", Priority = "medium" }
            };

            var arrayResultAsc = new[]
            {
                new { Video = "Video 8", Priority = "medium" },
                new { Video = "Video 5", Priority = "high" },
                new { Video = "Video 6", Priority = "high" },
                new { Video = "Video 7", Priority = "high" }
            };

            return Ok(priority == "asc" ? arrayResultAsc : arrayResultDesc);
        }

        [HttpGet("{userId}/videos/{videoId}")]
        public IActionResult Paths(int userId, int videoId, string priority = "desc")
        {
            var arrayResultDesc = new[]
            {
                new { Path = $"users/{userId}/groups/5/flows/4/videos/{videoId}", Priority = "high" },
                new { Path = $"users/{userId}/flows/4/videos/{videoId}", Priority = "medium" },
                new { Path = $"users/{userId}/videos/{videoId}", Priority = "low" }
            };

            var arrayResultAsc = new[]
            {
                new { Path = $"users/{userId}/videos/{videoId}", Priority = "low" },
                new { Path = $"users/{userId}/flows/4/videos/{videoId}", Priority = "medium" },
                new { Path = $"users/{userId}/groups/5/flows/4/videos/{videoId}", Priority = "high" }
            };

            return Ok(priority == "asc" ? arrayResultAsc : arrayResultDesc);
        }
    }
}
