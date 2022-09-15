using System;
using System.Collections.Generic;
using Calendar.Core.EventDescriptors;
using Calendar.Core.EventDescriptors.EventOccurrence;
using Calendar.Domain;
using FluentAssertions;
using Xunit;

namespace Calendar.Tests
{
    public class WeeklyOccurrencesGeneratorTests
    {
        private readonly RepeatMode _repeatMode;

        public WeeklyOccurrencesGeneratorTests()
        {
            _repeatMode = new WeeklyRepeatMode(new EventStartInformation {Date = new DateTime(2019, 10, 6)});
        }

        public static IEnumerable<object[]> Data
        {
            get
            {
                return new List<object[]>
                {
                    new object[]
                        {new DateTime(2019, 10, 6), new CalendarEventOccurrenceInfo(0, new DateTime(2019, 10, 6))},
                    new object[]
                        {new DateTime(2019, 10, 5), new CalendarEventOccurrenceInfo(0, new DateTime(2019, 10, 6))},
                    new object[]
                        {new DateTime(2019, 10, 7), new CalendarEventOccurrenceInfo(1, new DateTime(2019, 10, 13))},
                    new object[]
                        {new DateTime(2019, 9, 20), new CalendarEventOccurrenceInfo(0, new DateTime(2019, 10, 6))},
                    new object[]
                        {new DateTime(2019, 10, 20), new CalendarEventOccurrenceInfo(2, new DateTime(2019, 10, 20))},
                    new object[]
                        {new DateTime(2019, 10, 21), new CalendarEventOccurrenceInfo(3, new DateTime(2019, 10, 27))}
                };
            }
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void WHEN_called_with_certain_date_THEN_valid_result_is_returned(DateTime requestedTime,
            CalendarEventOccurrenceInfo info)
        {
            var result = _repeatMode.GetNearestOccurenceInfo(requestedTime);
            result.Result.Should().Be(info);
        }
    }
}