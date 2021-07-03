using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Hackaton.DataAccess.Entities
{
    public class VideoEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Priority? Priority { get; set; }
    }
}
