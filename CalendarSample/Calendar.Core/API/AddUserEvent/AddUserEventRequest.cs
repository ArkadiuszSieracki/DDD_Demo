using Communication.Core;
using Session.Core;

namespace Calendar.Core.API.AddUserEvent
{
    public class AddUserEventRequest : ServiceRequest<IAddUserEventProcessor>
    {
        public AddUserEventRequest(SessionIdentifier sessionIdentifier, CalendarEvent calendarEvent)
        {
            SessionIdentifier = sessionIdentifier;
            CalendarEvent = calendarEvent;
        }

        public SessionIdentifier SessionIdentifier { get; }
        public CalendarEvent CalendarEvent { get; }
    }
}