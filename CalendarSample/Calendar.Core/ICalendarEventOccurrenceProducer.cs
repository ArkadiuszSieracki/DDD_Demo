using System;
using System.Collections.Generic;
using Calendar.Core.EventDescriptors.EventOccurrence;

namespace Calendar.Core
{
    public interface ICalendarEventOccurrenceProducer
    {
        IEnumerable<CalendarEventOccurrence> GetAsOfDates(CalendarEvent calendarEvent, DateTime startTime,
            DateTime stopTime);
    }
}