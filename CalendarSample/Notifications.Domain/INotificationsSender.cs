using Calendar.Core;
using Calendar.Core.EventDescriptors.EventOccurrence;

namespace Notifications.Domain
{
    public interface INotificationsSender
    {
        void ScheduleSend(string name, CalendarEvent calendarEvent, CalendarEventOccurrence occurrence);
    }
}