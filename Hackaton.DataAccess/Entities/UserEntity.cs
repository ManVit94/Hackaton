using System.Collections.Generic;

using MongoDB.Bson.Serialization.Attributes;

namespace Hackaton.DataAccess.Entities
{
    public class UserEntity
    {
        [BsonId]
        public int Id { get; set; }

        public ICollection<GroupEntity> Groups { get; set; }

        public ICollection<VideoEntity> Videos { get; set; }

        public ICollection<FlowEntity> Flows { get; set; }

    }
}
