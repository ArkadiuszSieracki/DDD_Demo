using System;
using System.Threading.Tasks;
using Calendar.Core;
using Calendar.Core.API.UpdateUserEvent;
using Communication.Core;
using Notifications.Core.AddUserEvent;
using Session.Core;
using Session.Core.API.GetSessionInfo;

namespace Calendar.Domain
{
    public class UpdateCalendarEventProcessor : IUpdateUserEventProcessor
    {
        private readonly IMessageBus _bus;
        private readonly ICalendarEventRepository _repo;

        public UpdateCalendarEventProcessor(ICalendarEventRepository repo, IMessageBus bus)
        {
            _repo = repo;
            _bus = bus;
        }

        public IServiceResponse Process(IServiceRequest serviceRequest)
        {
            var request = serviceRequest as UpdateUserEventRequest;
            if (request == null)
            {
                return new ErrorResponse(new ServiceResult($"{nameof(UpdateCalendarEventProcessor)}:{nameof(this.Process)}:InvalidRequest", new Exception()));
            }
            var events =
                _repo.UpdateCalendarEvent(GetUserData(request.SessionIdentifier).Result, request.CalendarEvent);
            if (events.IsError())
            {
                return new ErrorResponse(events);
            }

            Task.Run(() => _bus.NotifyEventDirty(request.SessionIdentifier, request.CalendarEvent, CrudState.Modify));
            return new UpdateUserEventResponse();
        }

        public virtual ServiceResult<string> GetUserData(SessionIdentifier sessionId)
        {
            return _bus.GetUserData(sessionId);
        }
    }
}