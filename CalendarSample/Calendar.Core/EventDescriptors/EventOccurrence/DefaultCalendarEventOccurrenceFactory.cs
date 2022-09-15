namespace Calendar.Core.EventDescriptors.EventOccurrence
{
    public class DefaultCalendarEventOccurrenceFactory : ICalendarEventOccurrenceFactory
    {
        public CalendarEventOccurrence Create(CalendarEventOccurrenceInfo occurrenceInfo, CalendarEvent calendarEvent)
        {
            return new CalendarEventOccurrence(occurrenceInfo, calendarEvent.DefaultContent, calendarEvent.DefaultTime);
        }
    }
}