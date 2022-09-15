using System.Collections.Generic;
using Communication.Core;

namespace Calendar.Core.API.GetAllUserEvents
{
    public class GetAllUserEventsResponse : IServiceResponse
    {
        public GetAllUserEventsResponse(List<CalendarEvent> events)
        {
            CalendarEvents = events;
        }

        public List<CalendarEvent> CalendarEvents { get; }
    }
}