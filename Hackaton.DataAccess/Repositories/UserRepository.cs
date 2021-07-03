using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hackaton.DataAccess.Entities;
using Hackaton.DataAccess.Interfaces;
using MongoDB.Driver;

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

        public async Task<bool> IsCollectionEmpty()
        {
            return await _users.CountDocumentsAsync(u => true) == 0;
        }
    }
}
