using System;
using System.Threading;
using Calendar.Core;
using Communication.Core;
using Session.Core;

namespace Notifications.Core.AddUserEvent
{
    public static class NotifyCalendarEventChangedContractExtensions
    {
        public static ServiceResult NotifyEventDirty(this IMessageBus bus, SessionIdentifier sessionIdentifier,
            CalendarEvent calendarEvent, CrudState state)
        {
            var source = new CancellationTokenSource();
            source.CancelAfter(TimeSpan.FromMilliseconds(5000));

            var result = bus
                .ExecuteContractAsync<NotifyCalendarEventChangedContract, NotifyCalendarEventChangedRequest,
                    NotifyCalendarEventChangedResponse, INotifyCalendarEventChangedProcessor>(
                    new NotifyCalendarEventChangedContract(),
                    new NotifyCalendarEventChangedRequest(sessionIdentifier, calendarEvent, state), source.Token)
                .ConfigureAwait(false).GetAwaiter().GetResult();
            return result;
        }
    }
}