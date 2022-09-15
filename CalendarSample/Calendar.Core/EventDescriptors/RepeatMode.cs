using System;
using Calendar.Core.EventDescriptors.EventOccurrence;
using Communication.Core;

namespace Calendar.Core.EventDescriptors
{
    public abstract class RepeatMode
    {
        public abstract ServiceResult<CalendarEventOccurrenceInfo> GetNearestOccurenceInfo(DateTime time);
    }
}