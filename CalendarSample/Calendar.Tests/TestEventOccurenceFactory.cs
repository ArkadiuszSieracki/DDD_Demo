using System;
using System.Linq;
using Calendar.Core;
using Calendar.Core.EventDescriptors;
using Calendar.Core.EventDescriptors.EventOccurrence;
using Calendar.Domain;
using FluentAssertions;
using Xunit;

namespace Calendar.Tests
{
    public class TestEventOccurenceFactory
    {
        public TestEventOccurenceFactory()
        {
            _factory = new CalendarEventOccurrenceProducer(new DefaultCalendarEventOccurrenceFactory());
            _calendarEvent = new CalendarEvent();
            _calendarEvent.DefaultContent = new EventContent {Content = "a"};
            var occurrenceInfo = new CalendarEventOccurrenceInfo(1, new DateTime(2019, 10, 13));

            _calendarEvent.CustomEventOccurrences.Add(new CalendarEventOccurrence(occurrenceInfo,
                new EventContent {Content = "b"}, _time));
            _calendarEvent.EndInformation = new EventNumberRestrictor(3);
            _calendarEvent.StartInformation = new EventStartInformation {Date = new DateTime(2019, 10, 6)};
            _calendarEvent.RepeatMode = new WeeklyRepeatMode(_calendarEvent.StartInformation);
        }

        private readonly CalendarEventOccurrenceProducer _factory;
        private readonly CalendarEvent _calendarEvent;
        private readonly TimeSpan _time = new TimeSpan(8, 20, 0);

        [Fact]
        public void WHEN_using_api_THEN_user_can_get_calendar_occurence()
        {
            var result = _factory.GetAsOfDates(_calendarEvent, new DateTime(2019, 9, 2), new DateTime(2019, 10, 24));
            result.Count().Should().Be(3, "Pointed by end info limit");
        }

        [Fact]
        public void WHEN_using_api_THEN_user_can_get_calendar_valid_occurence()
        {
            var result = _factory.GetAsOfDates(_calendarEvent, new DateTime(2019, 12, 2), new DateTime(2019, 12, 24));
            result.Count().Should().Be(0, "Exceeded");
        }
    }
}