using Calendar.Core;
using Notifications.Core.AddUserEvent;

namespace Notifications.Domain
{
    public interface ICalendarNotificationsBuffer
    {
        void PopulateNotifications(string name, CalendarEvent calendarEvent, CrudState requestState);
    }
}