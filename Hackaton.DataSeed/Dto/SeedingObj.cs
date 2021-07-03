using System;
using System.Collections.Generic;
using System.Text;

namespace Hackaton.DataSeed.Dto
{
    public class SeedingObj
    {
        public ICollection<User> Users { get; set; }
        public ICollection<Video> Videos { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Flow> Flows { get; set; }
        public ICollection<UserVideo> UsersToVideos { get; set; }
        public ICollection<UserGroup> UsersToGroups { get; set; }
        public ICollection<UserFlow> UsersToFlows { get; set; }
        public ICollection<GroupVideo> GroupsToVideos { get; set; }
        public ICollection<GroupFlow> GroupsToFlows { get; set; }
        public ICollection<FlowVideo> FlowsToVideos { get; set; }
    }
}
