using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Calendar.Core;
using Calendar.Core.EventDescriptors.EventOccurrence;
using Notifications.Core.AddUserEvent;

namespace Notifications.Domain
{
    public class CalendarNotificationsBuffer : ICalendarNotificationsBuffer
    {
        private readonly LinkedList<NotificationInfo> _dataItems = new LinkedList<NotificationInfo>();


        private readonly IDateTimeProvider _dateTimeProvider;

        private readonly ICalendarEventOccurrenceProducer _producer;

        private readonly ICalendarEventRepository _repository;
        private readonly INotificationsSender _sender;

        public CalendarNotificationsBuffer(
            ICalendarEventRepository repository,
            ICalendarEventOccurrenceProducer producer,
            IDateTimeProvider dateTimeProvider,
            INotificationsSender sender)
        {
            _repository = repository;
            _producer = producer;
            _dateTimeProvider = dateTimeProvider;
            _sender = sender;
        }

        public void PopulateNotifications(string name, CalendarEvent calendarEvent, CrudState requestState)
        {
            var occurrences = _producer.GetAsOfDates(calendarEvent,
                _dateTimeProvider.GetCurrentDate(),
                _dateTimeProvider.GetCurrentDate().AddDays(1));
            DeleteOldNotifications(calendarEvent);
            if (requestState == CrudState.Remove)
            {
                return;
            }

            foreach (var occurence in occurrences)
            {
                ScheduleNewNotification(name, calendarEvent, occurence);
            }
        }


        public void GenerateMesssages()
        {
            foreach (var calendarEventInfo in _repository.GetAllCalendarInfo())
            {
                PopulateNotifications(calendarEventInfo.user, calendarEventInfo.calendarEvent, CrudState.Modify);
            }
        }

        private void ScheduleNewNotification(string name, CalendarEvent calendarEvent,
            CalendarEventOccurrence occurence)
        {
            var source = new CancellationTokenSource();

            var targetTime = occurence.Info.OccurrenceDate.Date;
            targetTime = targetTime.Add(occurence.Time);
            var currentTime = _dateTimeProvider.GetCurrentDateTime();

            if (currentTime < targetTime)
            {
                var notification = new NotificationInfo(name, calendarEvent, occurence, source);
                lock (_dataItems)
                {
                    _dataItems.AddLast(notification);
                }

                var span = targetTime.Subtract(_dateTimeProvider.GetCurrentDateTime());

                Task.Delay(span, source.Token).ContinueWith(
                    o =>
                    {
                        if (o.IsCanceled == false)
                        {
                            _sender.ScheduleSend(notification.User, notification.CalendarEvent, notification.Occurence);
                        }
                    }, source.Token);
            }
        }

        private void DeleteOldNotifications(CalendarEvent calendarEvent)
        {
            lock (_dataItems)
            {
                foreach (var item in _dataItems.Where(o => o.CalendarEvent.Id == calendarEvent.Id).ToList())
                {
                    item.Cancel();
                    _dataItems.Remove(item);
                }
            }
        }

        private class NotificationInfo
        {
            private readonly CancellationTokenSource _cancellationTokenSource;

            public NotificationInfo(string user, CalendarEvent calendarEvent, CalendarEventOccurrence occurence,
                CancellationTokenSource cancellationTokenSource)
            {
                _cancellationTokenSource = cancellationTokenSource;
                User = user;
                CalendarEvent = calendarEvent;
                Occurence = occurence;
            }

            public CalendarEventOccurrence Occurence { get; }

            public string User { get; }
            public CalendarEvent CalendarEvent { get; }

            public void Cancel()
            {
                _cancellationTokenSource.Cancel();
            }
        }
    }
}