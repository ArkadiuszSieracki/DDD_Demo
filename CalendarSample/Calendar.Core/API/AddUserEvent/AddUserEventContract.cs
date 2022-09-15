using Communication.Core;

namespace Calendar.Core.API.AddUserEvent
{
    public class
        AddUserEventContract : ServiceContract<AddUserEventRequest, AddUserEventResponse, IAddUserEventProcessor>
    {
    }
}