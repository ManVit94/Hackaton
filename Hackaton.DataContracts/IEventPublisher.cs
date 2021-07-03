using Hackaton.DataContracts.Messages;

namespace Hackaton.DataContracts
{
    public interface IEventPublisher
    {
        void Publish(RequestCalledEvent @event);
    }
}
