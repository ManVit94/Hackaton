using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Hackaton.Business.Dto;
using Hackaton.Business.Interfaces;
using Hackaton.DataAccess.Entities;
using Hackaton.DataAccess.Interfaces;

namespace Hackaton.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<VideoDto>> GetVideosAsync(int userId, string priority)
        {
            var user = await _repository.GetUser(userId);

            var allVideos = new List<VideoEntity>(user.Videos);

            var videosFromFlows = user.Flows.SelectMany(f => f.Videos.Select(v => new VideoEntity
            {
                Id = v.Id,
                Name = v.Name,
                Priority = f.Priority
            }));
            var videosFromGrops = user.Groups.SelectMany(g => g.Videos);
            var videosFromGropsFlows = user.Groups.SelectMany(g => g.Flows.SelectMany(f => f.Videos.Select(v => new VideoEntity
            {
                Id = v.Id,
                Name = v.Name,
                Priority = f.Priority
            })));

            allVideos.AddRange(videosFromFlows);
            allVideos.AddRange(videosFromGrops);
            allVideos.AddRange(videosFromGropsFlows);

            var distinctVideos = allVideos.GroupBy(v => v.Name).Select(g => new VideoDto { Video = g.Key, Priority = g.Max(v => v.Priority.Value) });

            return priority == "asc" ?
                distinctVideos.OrderBy(r => r.Priority).ThenBy(r => r.Video).ToArray() :
                distinctVideos.OrderByDescending(r => r.Priority).ThenBy(r => r.Video).ToArray();
        }

        public async Task<ICollection<PathDto>> GetPathsToVideAsync(int userId, int videoId, string priority)
        {
            var user = await _repository.GetUser(userId);

            var result = new List<PathDto>();

            AddPathFromVideos(result, user, videoId);

            AddPathsFromGroups(result, user, videoId);

            AddPathsFromGropsFlows(result, user, videoId);

            AddPathsFromFlows(result, user, videoId);

            return priority == "asc" ? 
                result.OrderBy(r => r.Priority).ToArray() :
                result.OrderByDescending(r => r.Priority).ToArray();
        }

        void AddPathFromVideos(List<PathDto> paths, UserEntity user, int videoId)
        {
            var videoPathFromVideos = user.Videos.FirstOrDefault(v => v.Id == videoId);

            if (videoPathFromVideos != null)
                paths.Add(new PathDto
                {
                    Path = $"users/{user.Id}/videos/{videoPathFromVideos.Id}",
                    Priority = videoPathFromVideos.Priority.Value
                });
        }

        void AddPathsFromGroups(List<PathDto> paths, UserEntity user, int videoId)
        {
            var videoPathsFromGroups = user.Groups
                .SelectMany(g => g.Videos.Where(v => v.Id == videoId), (g, v) => new { GroupdId = g.Id, Video = v });

            foreach (var item in videoPathsFromGroups)
            {
                paths.Add(new PathDto
                {
                    Path = $"users/{user.Id}/groups/{item.GroupdId}/videos/{item.Video.Id}",
                    Priority = item.Video.Priority.Value
                });
            }
        }

        void AddPathsFromGropsFlows(List<PathDto> paths, UserEntity user, int videoId)
        {
            var videoPathsFromGropsFlows = user.Groups.SelectMany(g => g.Flows, (g, f) => new { Flow = f, GroupId = g.Id })
                .SelectMany(o => o.Flow.Videos.Where(v => v.Id == videoId),
                (o, v) => new
                {
                    FlowId = o.Flow.Id,
                    GroupdId = o.GroupId,
                    Video = new VideoEntity
                    {
                        Id = v.Id,
                        Name = v.Name,
                        Priority = o.Flow.Priority
                    }
                });

            foreach (var item in videoPathsFromGropsFlows)
            {
                paths.Add(new PathDto
                {
                    Path = $"users/{user.Id}/groups/{item.GroupdId}/flows/{item.FlowId}/videos/{item.Video.Id}",
                    Priority = item.Video.Priority.Value
                });
            }
        }

        void AddPathsFromFlows(List<PathDto> paths, UserEntity user, int videoId)
        {
            var videoPathsFromFlows = user.Flows.SelectMany(f => f.Videos.Where(v => v.Id == videoId), (f, v) => new
            {
                FlowId = f.Id,
                Video = new VideoEntity
                {
                    Id = v.Id,
                    Name = v.Name,
                    Priority = f.Priority
                }
            });

            foreach (var item in videoPathsFromFlows)
            {
                paths.Add(new PathDto
                {
                    Path = $"users/{user.Id}/flows/{item.FlowId}/videos/{item.Video.Id}",
                    Priority = item.Video.Priority.Value
                });
            }
        }
    }
}
