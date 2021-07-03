using Hackaton.DataAccess;

namespace Hackaton.DataSeed.Dto
{
    class UserFlow
    {
        public int UserId { get; set; }
        public int FlowId { get; set; }
        public Priority Priority { get; set; }
    }
}
