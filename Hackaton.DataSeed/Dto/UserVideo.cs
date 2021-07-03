using System;
using System.Collections.Generic;
using System.Text;
using Hackaton.DataAccess;

namespace Hackaton.DataSeed.Dto
{
    public class UserVideo
    {
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public Priority Priority { get; set; }
    }
}
