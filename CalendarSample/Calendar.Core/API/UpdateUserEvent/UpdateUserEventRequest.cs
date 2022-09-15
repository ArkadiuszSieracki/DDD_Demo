using Communication.Core;
using Session.Core;

namespace Calendar.Core.API.UpdateUserEvent
{
    public class UpdateUserEventRequest : ServiceRequest<IUpdateUserEventProcessor>
    {
        public UpdateUserEventRequest(SessionIdentifier sessionIdentifier, CalendarEvent calendarEvent)
        {
            SessionIdentifier = sessionIdentifier;
            CalendarEvent = calendarEvent;
        }

        public SessionIdentifier SessionIdentifier { get; }
        public CalendarEvent CalendarEvent { get; }
    }
}