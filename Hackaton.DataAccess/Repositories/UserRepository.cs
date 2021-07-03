using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Driver;

using Hackaton.DataAccess.Entities;
using Hackaton.DataAccess.Interfaces;

namespace Hackaton.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserEntity> _users;

        public UserRepository(IMongoCollection<UserEntity> users)
        {
            _users = users;
        }

        public Task AddManyAsync(IEnumerable<UserEntity> users)
        {
            return _users.InsertManyAsync(users);
        }

        public Task<UserEntity> GetUser(int id)
        {
            return _users.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> IsCollectionEmpty()
        {
            return await _users.CountDocumentsAsync(u => true) == 0;
        }
    }
}
