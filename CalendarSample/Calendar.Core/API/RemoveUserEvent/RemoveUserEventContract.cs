using Communication.Core;

namespace Calendar.Core.API.RemoveUserEvent
{
    public class RemoveUserEventContract : ServiceContract<RemoveUserEventRequest, RemoveUserEventResponse,
        IRemoveUserEventProcessor>
    {
    }
}