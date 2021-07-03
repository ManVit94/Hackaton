using System.Collections.Generic;
using System.Threading.Tasks;

using Hackaton.DataAccess.Entities;

namespace Hackaton.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsCollectionEmpty();
        Task AddManyAsync(IEnumerable<UserEntity> users);
        Task<UserEntity> GetUser(int id);
    }
}
