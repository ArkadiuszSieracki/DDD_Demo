using System;
using Communication.Core;
using Session.Core;

namespace Calendar.Core.API.GetAllUserEvents
{
    public class GetAllUserEventsRequest : ServiceRequest<IGetAllUserEventsProcessor>
    {
        public GetAllUserEventsRequest(SessionIdentifier sessionIdentifier, DateTime startTime, DateTime endTime)
        {
            SessionIdentifier = sessionIdentifier;
            StartTime = startTime;
            EndTime = endTime;
        }

        public SessionIdentifier SessionIdentifier { get; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}