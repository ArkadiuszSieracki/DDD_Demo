using System;
using System.Collections.Generic;
using System.Linq;
using Calendar.Core;
using Calendar.Core.EventDescriptors.EventOccurrence;

namespace Calendar.Domain
{
    public class CalendarEventOccurrenceProducer : ICalendarEventOccurrenceProducer
    {
        private readonly ICalendarEventOccurrenceFactory _defaultEventOccurrenceFactory;

        public CalendarEventOccurrenceProducer(ICalendarEventOccurrenceFactory defaultEventOccurrenceFactory)
        {
            _defaultEventOccurrenceFactory = defaultEventOccurrenceFactory;
        }

        public IEnumerable<CalendarEventOccurrence> GetAsOfDates(CalendarEvent calendarEvent, DateTime startTime,
            DateTime stopTime)
        {
            var occurrences = GetEventOccurrences(calendarEvent, startTime, stopTime);
            var customOccurrences = calendarEvent.CustomEventOccurrences.ToDictionary(o => o.Info, o => o);
            foreach (var calendarEventOccurrenceInfo in occurrences)
            {
                if (calendarEvent.EndInformation.IsFinished(calendarEventOccurrenceInfo))
                {
                    if (customOccurrences.ContainsKey(calendarEventOccurrenceInfo))
                    {
                        yield return customOccurrences[calendarEventOccurrenceInfo];
                    }
                    else
                    {
                        yield return _defaultEventOccurrenceFactory.Create(calendarEventOccurrenceInfo, calendarEvent);
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public IEnumerable<CalendarEventOccurrenceInfo> GetEventOccurrences(CalendarEvent calendarEvent,
            DateTime startTime, DateTime stopTime)
        {
            var startInfo = calendarEvent.RepeatMode.GetNearestOccurenceInfo(startTime);
            while (stopTime > startInfo.Result.OccurrenceDate)
            {
                startTime = startInfo.Result.OccurrenceDate.AddDays(1);
                yield return startInfo.Result;
                startInfo = calendarEvent.RepeatMode.GetNearestOccurenceInfo(startTime);
            }
        }
    }
}