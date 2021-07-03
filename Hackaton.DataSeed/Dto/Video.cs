using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Hackaton.DataSeed.Dto
{
    public class Video
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
