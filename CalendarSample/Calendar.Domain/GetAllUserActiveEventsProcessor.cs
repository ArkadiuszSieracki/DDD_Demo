using System;
using System.Linq;
using Calendar.Core;
using Calendar.Core.API.GetAllUserEvents;
using Communication.Core;
using Session.Core;
using Session.Core.API.GetSessionInfo;

namespace Calendar.Domain
{
    public class GetAllUserActiveEventsProcessor : IGetAllUserEventsProcessor
    {
        private readonly IMessageBus _bus;
        private readonly ICalendarEventOccurrenceProducer _producer;
        private readonly ICalendarEventRepository _repo;

        public GetAllUserActiveEventsProcessor(ICalendarEventRepository repo, IMessageBus bus,
            ICalendarEventOccurrenceProducer producer)
        {
            _repo = repo;
            _bus = bus;
            _producer = producer;
        }

        public IServiceResponse Process(IServiceRequest serviceRequest)
        {
            var request = serviceRequest as GetAllUserEventsRequest;
            if (request == null)
            {
                return new ErrorResponse(new ServiceResult($"{nameof(GetAllUserActiveEventsProcessor)}:{nameof(this.Process)}:InvalidRequest", new Exception()));
            }
            var events = _repo.GetCalendarEvent(GetUserData(request.SessionIdentifier).Result);
            if (events.IsError())
            {
                return new ErrorResponse(events);
            }

            var activEvents = events.Result
                .Where(o => _producer.GetAsOfDates(o, request.StartTime, request.EndTime).Any()).ToList();
            return new GetAllUserEventsResponse(activEvents);
        }

        public virtual ServiceResult<string> GetUserData(SessionIdentifier sessionId)
        {
            return _bus.GetUserData(sessionId);
        }
    }
}