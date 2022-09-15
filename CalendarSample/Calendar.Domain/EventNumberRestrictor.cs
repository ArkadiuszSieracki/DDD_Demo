using Calendar.Core.EventDescriptors;
using Calendar.Core.EventDescriptors.EventOccurrence;

namespace Calendar.Domain
{
    public class EventNumberRestrictor : IEventEndInformation
    {
        public EventNumberRestrictor(int maxOccurrencesNumber)
        {
            MaxOccurrencesNumber = maxOccurrencesNumber;
        }

        public int MaxOccurrencesNumber { get; set; }

        public bool IsFinished(CalendarEventOccurrenceInfo calendarEventOccurrenceInfo)
        {
            return calendarEventOccurrenceInfo.SequenceNumber < MaxOccurrencesNumber;
        }
    }
}