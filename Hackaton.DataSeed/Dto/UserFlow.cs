using System;
using System.Collections.Generic;
using System.Text;
using Hackaton.DataAccess;

namespace Hackaton.DataSeed.Dto
{
    public class UserFlow
    {
        public int UserId { get; set; }
        public int FlowId { get; set; }
        public Priority Priority { get; set; }
    }
}
