using System;

namespace Calendar.Core.EventDescriptors.EventOccurrence
{
    public class CalendarEventOccurrenceInfo
    {
        public CalendarEventOccurrenceInfo(int eventNo, DateTime evendDate)
        {
            SequenceNumber = eventNo;
            OccurrenceDate = evendDate;
        }

        public DateTime OccurrenceDate { get; }

        public int SequenceNumber { get; }

        public override int GetHashCode()
        {
            return SequenceNumber * 327 + OccurrenceDate.DayOfYear * 523;
        }

        public override bool Equals(object obj)
        {
            var typeObject = obj as CalendarEventOccurrenceInfo;
            if (typeObject != null)
            {
                return SequenceNumber.Equals(typeObject.SequenceNumber) &&
                       OccurrenceDate.Date.Equals(typeObject.OccurrenceDate.Date);
            }

            return false;
        }
    }
}