using Communication.Core;
using Session.Core;

namespace Calendar.Core.API.RemoveUserEvent
{
    public class RemoveUserEventRequest : ServiceRequest<IRemoveUserEventProcessor>
    {
        public RemoveUserEventRequest(SessionIdentifier sessionIdentifier, CalendarEvent calendarEvent)
        {
            SessionIdentifier = sessionIdentifier;
            CalendarEvent = calendarEvent;
        }

        public SessionIdentifier SessionIdentifier { get; }
        public CalendarEvent CalendarEvent { get; }
    }
}