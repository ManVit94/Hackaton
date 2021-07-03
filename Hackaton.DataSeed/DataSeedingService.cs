using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hackaton.DataAccess.Entities;
using Hackaton.DataAccess.Interfaces;
using Hackaton.DataSeed.Dto;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Hackaton.DataSeed
{
    public class DataSeedingService : IDataSeedingService
    {
        private readonly IUserRepository _userRepository;

        private IDictionary<int, Video> _videos;

        private SeedingObj _seedingObj;

        public DataSeedingService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task SeedInitialData()
        {
            if (!await _userRepository.IsCollectionEmpty())
                return;

            var data = await File.ReadAllTextAsync("Data.json");

            var users = ParseUssers(data);

            await _userRepository.AddManyAsync(users);
        }

        ICollection<UserEntity> ParseUssers(string data)
        {
            _seedingObj = JsonConvert.DeserializeObject<SeedingObj>(data);

            _videos = _seedingObj.Videos.ToDictionary(v => v.Id);

            var userEntities = new List<UserEntity>();

            foreach (var user in _seedingObj.Users)
            {
                var userEntity = new UserEntity { Id = user.Id };

                ParseUserVideos(userEntity);

                ParseUserFlows(userEntity);

                ParseUserGroups(userEntity);

                userEntities.Add(userEntity);
            }

            return userEntities;
        }

        void ParseUserVideos(UserEntity userEntity)
        {
            userEntity.Videos = _seedingObj.UsersToVideos
                    .Where(uv => uv.UserId == userEntity.Id)
                    .Select(uv => new VideoEntity
                    {
                        Id = uv.VideoId,
                        Priority = uv.Priority,
                        Name = _videos[uv.VideoId].Name
                    })
                    .ToArray();
        }

        void ParseUserFlows(UserEntity userEntity)
        {
            userEntity.Flows = _seedingObj.UsersToFlows
                    .Where(uf => uf.UserId == userEntity.Id)
                    .Select(uf => new FlowEntity
                    {
                        Id = uf.FlowId,
                        Priority = uf.Priority,
                    })
                    .ToArray();

            ParseFlows(userEntity.Flows);
        }

        void ParseUserGroups(UserEntity userEntity)
        {
            userEntity.Groups = _seedingObj.UsersToGroups
                    .Where(ug => ug.UserId == userEntity.Id)
                    .Select(ug => new GroupEntity
                    {
                        Id = ug.GroupId
                    })
                    .ToArray();

            foreach (var group in userEntity.Groups)
            {
                group.Videos = _seedingObj.GroupsToVideos
                    .Where(gv => gv.GroupId == group.Id)
                    .Select(gv => new VideoEntity
                    {
                        Id = gv.VideoId,
                        Priority = gv.Priority,
                        Name = _videos[gv.VideoId].Name
                    })
                    .ToArray();

                group.Flows = _seedingObj.GroupsToFlows
                    .Where(gf => gf.GroupId == group.Id)
                    .Select(gf => new FlowEntity
                    {
                        Id = gf.FlowId,
                        Priority = gf.Priority,
                    })
                    .ToArray();

                ParseFlows(group.Flows);
            }
        }

        void ParseFlows(ICollection<FlowEntity> flows)
        {
            foreach (var flow in flows)
            {
                flow.Videos = _seedingObj.FlowsToVideos
                    .Where(fv => fv.FlowId == flow.Id)
                    .Select(fv => new VideoEntity
                    {
                        Id = fv.VideoId,
                        Name = _videos[fv.VideoId].Name
                    })
                    .ToArray();
            }
        }
    }
}
