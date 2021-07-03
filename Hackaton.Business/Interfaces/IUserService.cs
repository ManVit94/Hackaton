using System.Collections.Generic;
using System.Threading.Tasks;

using Hackaton.Business.Dto;

namespace Hackaton.Business.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<VideoDto>> GetVideosAsync(int userId, string priority);
        Task<ICollection<PathDto>> GetPathsToVideAsync(int userId, int videoId, string priority);
    }
}
