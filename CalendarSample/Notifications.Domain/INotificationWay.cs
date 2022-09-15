using System.Collections.Generic;
using Calendar.Core;
using Communication.Core;

namespace Notifications.Domain
{
    public interface INotificationWay
    {
        void Notify(CalendarEvent dataCalendarEvent, IEnumerable<IHost> select);
    }
}