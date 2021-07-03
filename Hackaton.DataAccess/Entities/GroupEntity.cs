using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Hackaton.DataAccess.Entities
{
    public class GroupEntity
    {
        public int Id { get; set; }
        public ICollection<FlowEntity> Flows { get; set; }
        public ICollection<VideoEntity> Videos { get; set; }
    }
}
