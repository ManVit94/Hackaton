using System.Collections.Generic;

namespace Hackaton.DataAccess.Entities
{
    public class FlowEntity
    {
        public int Id { get; set; }
        public ICollection<VideoEntity> Videos { get; set; }
        public Priority Priority { get; set; }
    }
}
