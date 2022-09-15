using Communication.Core;

namespace Calendar.Core.API.GetAllUserEvents
{
    public class GetAllUserEventsContract : ServiceContract<GetAllUserEventsRequest, GetAllUserEventsResponse,
        IGetAllUserEventsProcessor>
    {
    }
}