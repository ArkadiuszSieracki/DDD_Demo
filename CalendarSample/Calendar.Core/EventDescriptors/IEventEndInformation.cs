using Calendar.Core.EventDescriptors.EventOccurrence;

namespace Calendar.Core.EventDescriptors
{
    public interface IEventEndInformation
    {
        bool IsFinished(CalendarEventOccurrenceInfo calendarEventOccurrenceInfo);
    }
}