using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Hackaton.DataAccess.Entities
{
    public class FlowEntity
    {
        public int Id { get; set; }
        public ICollection<VideoEntity> Videos { get; set; }
        public Priority Priority { get; set; }
    }
}
