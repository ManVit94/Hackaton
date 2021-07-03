using System;
using System.Collections.Generic;
using System.Text;
using Hackaton.DataAccess;

namespace Hackaton.DataSeed.Dto
{
    public class GroupVideo
    {
        public int GroupId { get; set; }
        public int VideoId { get; set; }
        public Priority Priority { get; set; }
    }
}
