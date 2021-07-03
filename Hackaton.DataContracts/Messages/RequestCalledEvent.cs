namespace Hackaton.DataContracts.Messages
{
    public class RequestCalledEvent
    {
        public string Url { get; set; }
        public int ItemsCount { get; set; }
        public long ElapsedTime { get; set; }
    }
}
