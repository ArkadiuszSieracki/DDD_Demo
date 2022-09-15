using System;
using Calendar.Core.EventDescriptors;
using Calendar.Core.EventDescriptors.EventOccurrence;
using Communication.Core;

namespace Calendar.Domain
{
    public class WeeklyRepeatMode : RepeatMode
    {
        private readonly EventStartInformation _startInformation;

        public WeeklyRepeatMode(EventStartInformation startInformation)
        {
            _startInformation = startInformation;
        }


        public override ServiceResult<CalendarEventOccurrenceInfo> GetNearestOccurenceInfo(DateTime requestedTime)
        {
            var span = _startInformation.Date.Date.Subtract(requestedTime);
            if (span.Days >= 0)
            {
                var result = new CalendarEventOccurrenceInfo(
                    0, _startInformation.Date.Date);
                return new ServiceResult<CalendarEventOccurrenceInfo>(result);
            }

            var eventNo = (int) Math.Ceiling(Math.Abs(span.Days / 7d));
            var datesOffset = Math.Abs(span.Days) - eventNo * 7;


            var nextDate = requestedTime.Date.Subtract(TimeSpan.FromDays(datesOffset));
            var nextOccurence = new CalendarEventOccurrenceInfo(
                eventNo, nextDate);
            return new ServiceResult<CalendarEventOccurrenceInfo>(nextOccurence);
        }
    }
}