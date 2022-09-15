using System;
using Communication.Core;
using Notifications.Core.AddUserEvent;
using Session.Core.API.GetSessionInfo;

namespace Notifications.Domain
{
    public class NotifyCalendarEventChangedProcessor : INotifyCalendarEventChangedProcessor
    {
        private readonly ICalendarNotificationsBuffer _buffer;
        private readonly IMessageBus _bus;

        public NotifyCalendarEventChangedProcessor(ICalendarNotificationsBuffer buffer, IMessageBus bus)
        {
            _buffer = buffer;
            _bus = bus;
        }

        public IServiceResponse Process(IServiceRequest serviceRequest)
        {
            var request = serviceRequest as NotifyCalendarEventChangedRequest;
            if (request == null)
            {
                return new ErrorResponse(new ServiceResult($"{nameof(NotifyCalendarEventChangedProcessor)}:{nameof(this.Process)}:InvalidRequest", new Exception()));
            }
            var result = _bus.GetUserData(request.SessionIdentifier).Result;

            _buffer.PopulateNotifications(result, request.CalendarEvent, request.State);
            return new NotifyCalendarEventChangedResponse();
        }
    }
}