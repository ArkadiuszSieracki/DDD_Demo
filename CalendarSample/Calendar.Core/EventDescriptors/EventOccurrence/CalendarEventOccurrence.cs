using System;

namespace Calendar.Core.EventDescriptors.EventOccurrence
{
    public class CalendarEventOccurrence
    {
        public CalendarEventOccurrence(CalendarEventOccurrenceInfo occurrenceInfo, EventContent defaultContent,
            TimeSpan time)
        {
            Info = occurrenceInfo;
            Content = defaultContent;
            Time = time;
        }

        public CalendarEventOccurrenceInfo Info { get; set; }

        public EventContent Content { get; set; }
        public TimeSpan Time { get; set; }
    }
}