using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calendar.Core;
using Calendar.Core.EventDescriptors.EventOccurrence;
using Communication.Core;
using Session.Core.API.GetAllSessionsInfo;

namespace Notifications.Domain
{
    public class NotificationsSender : INotificationsSender
    {
        private readonly IMessageBus _bus;

        // A bounded collection. It can hold no more 
        // than 100 items at once.
        private readonly
            BlockingCollection<(string user, CalendarEvent calendarEvent, CalendarEventOccurrence occurrence)>
            _dataItems
                =
                new BlockingCollection<(string user, CalendarEvent calendarEvent, CalendarEventOccurrence occurrence)
                >();

        private readonly IEnumerable<INotificationWay> _notificationWays;

        public NotificationsSender(
            IMessageBus bus,
            IEnumerable<INotificationWay> notificationWays
        )
        {
            _bus = bus;
            _notificationWays = notificationWays;

            ProcessSendNotifications();
        }

        public void ScheduleSend(string name, CalendarEvent calendarEvent, CalendarEventOccurrence occurrence)
        {
            _dataItems.Add((name, calendarEvent, occurrence));
        }

        private void ProcessSendNotifications()
        {
            Task.Run(() =>
            {
                while (!_dataItems.IsCompleted)
                {
                    (string user, CalendarEvent calendarEvent, CalendarEventOccurrence occurrence) data = (null, null,
                        null);
                    var isValid = true;
                    // Blocks if dataItems.Count == 0.
                    // IOE means that Take() was called on a completed collection.
                    // Some other thread can call CompleteAdding after we pass the
                    // IsCompleted check but before we call Take. 
                    // In this example, we can simply catch the exception since the 
                    // loop will break on the next iteration.
                    try
                    {
                        data = _dataItems.Take();
                    }
                    catch (InvalidOperationException)
                    {
                        isValid = false;
                    }

                    if (isValid)
                    {
                        ProcessElement(data);
                    }
                }
            });
        }

        private void ProcessElement((string user, CalendarEvent calendarEvent, CalendarEventOccurrence occurrence) data)
        {
            var allSessionsData = _bus.GetAllSessionsData();
            var userSessions = allSessionsData.Result.Where(o => o.Name == data.user).ToArray();
            foreach (var notificationWay in _notificationWays)
            {
                notificationWay.Notify(data.calendarEvent, userSessions.Select(o => o.Host));
            }
        }
    }
}