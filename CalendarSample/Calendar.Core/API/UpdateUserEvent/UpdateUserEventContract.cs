using Communication.Core;

namespace Calendar.Core.API.UpdateUserEvent
{
    public class UpdateUserEventContract : ServiceContract<UpdateUserEventRequest, UpdateUserEventResponse,
        IUpdateUserEventProcessor>
    {
    }
}