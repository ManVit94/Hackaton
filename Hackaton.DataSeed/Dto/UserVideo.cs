using Hackaton.DataAccess;

namespace Hackaton.DataSeed.Dto
{
    class UserVideo
    {
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public Priority Priority { get; set; }
    }
}
