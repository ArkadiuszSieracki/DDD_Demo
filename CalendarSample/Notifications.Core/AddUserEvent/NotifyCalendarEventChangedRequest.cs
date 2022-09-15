using Calendar.Core;
using Communication.Core;
using Session.Core;

namespace Notifications.Core.AddUserEvent
{
    public class NotifyCalendarEventChangedRequest : ServiceRequest<INotifyCalendarEventChangedProcessor>
    {
        public NotifyCalendarEventChangedRequest(SessionIdentifier sessionIdentifier, CalendarEvent calendarEvent,
            CrudState state)
        {
            State = state;
            SessionIdentifier = sessionIdentifier;
            CalendarEvent = calendarEvent;
        }

        public CrudState State { get; }
        public SessionIdentifier SessionIdentifier { get; }
        public CalendarEvent CalendarEvent { get; }
    }
}