using System;
using System.Collections.Generic;
using Calendar.Core.EventDescriptors;
using Calendar.Core.EventDescriptors.EventOccurrence;

namespace Calendar.Core
{
    public class CalendarEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public RepeatMode RepeatMode { get; set; }

        public EventContent DefaultContent { get; set; }

        public List<CalendarEventOccurrence> CustomEventOccurrences { get; set; } = new List<CalendarEventOccurrence>();

        public IEventEndInformation EndInformation { get; set; }

        public EventStartInformation StartInformation { get; set; }
        public TimeSpan DefaultTime { get; set; }
    }
}