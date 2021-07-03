using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hackaton.DataAccess.Entities;

namespace Hackaton.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsCollectionEmpty();
        Task AddManyAsync(IEnumerable<UserEntity> users);
    }
}
