using System;
using System.Threading.Tasks;
using Calendar.Core;
using Calendar.Core.API.RemoveUserEvent;
using Communication.Core;
using Notifications.Core.AddUserEvent;
using Session.Core;
using Session.Core.API.GetSessionInfo;

namespace Calendar.Domain
{
    public class RemoveCalendarEventProcessor : IRemoveUserEventProcessor
    {
        private readonly IMessageBus _bus;
        private readonly ICalendarEventRepository _repo;

        public RemoveCalendarEventProcessor(ICalendarEventRepository repo, IMessageBus bus)
        {
            _repo = repo;
            _bus = bus;
        }

        public IServiceResponse Process(IServiceRequest ServiceRequest)
        {
            var request = ServiceRequest as RemoveUserEventRequest;
            if (request == null)
            {
                return new ErrorResponse(new ServiceResult($"{nameof(RemoveCalendarEventProcessor)}:{nameof(this.Process)}:InvalidRequest", new Exception()));
            }
            var events =
                _repo.DeleteCalendarEvent(GetUserData(request.SessionIdentifier).Result, request.CalendarEvent);
            if (events.IsError())
            {
                return new ErrorResponse(events);
            }

            Task.Run(() => _bus.NotifyEventDirty(request.SessionIdentifier, request.CalendarEvent, CrudState.Remove));
            return new RemoveUserEventResponse();
            ;
        }

        public virtual ServiceResult<string> GetUserData(SessionIdentifier sessionId)
        {
            return _bus.GetUserData(sessionId);
        }
    }
}