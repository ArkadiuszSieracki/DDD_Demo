using System.Collections.Generic;
using Calendar.Core;
using Communication.Core;
using Notifications.Domain;

namespace Integration.Tests
{
    internal class TestNotificationsWay : INotificationWay
    {
        public int NotificationsCount { get; set; }

        public void Notify(CalendarEvent dataCalendarEvent, IEnumerable<IHost> select)
        {
            NotificationsCount++;
        }
    }
}