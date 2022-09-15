namespace Calendar.Core.EventDescriptors.EventOccurrence
{
    public interface ICalendarEventOccurrenceFactory
    {
        CalendarEventOccurrence Create(CalendarEventOccurrenceInfo occurrenceInfo, CalendarEvent calendarEvent);
    }
}