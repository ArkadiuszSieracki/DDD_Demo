using System;
using System.Linq;
using Authentication.Core;
using Calendar.Core;
using Calendar.Core.EventDescriptors;
using Calendar.Core.EventDescriptors.EventOccurrence;
using Calendar.Domain;
using Communication.Core;
using FluentAssertions;
using Notifications.Domain;
using NSubstitute;
using Session.Core.API.CreateSession;
using Xunit;

namespace Integration.Tests
{
    public class UnitTest1 : TestClassBase
    {
        private readonly TimeSpan _time = new TimeSpan(8, 20, 0);
        //TODO: test how is app scaling with different numbers of hosts with different configurations
        private CalendarEvent GetSomeEvent()
        {
            var calendarEvent = new CalendarEvent();
            calendarEvent.DefaultTime = _time;
            calendarEvent.DefaultContent = new EventContent {Content = "a"};
            calendarEvent.EndInformation = new EventNumberRestrictor(3);
            calendarEvent.StartInformation = new EventStartInformation {Date = new DateTime(2019, 10, 6)};
            calendarEvent.RepeatMode = new WeeklyRepeatMode(calendarEvent.StartInformation);
            return calendarEvent;
        }


        //TODO: fix race condition
        [Fact]
        public void WHEN__user_is_logged_in_THAN_he_can_do_crud_operations_on_calendar_and_notification_is_sent()
        {
            var host = GetHost();
            var eventDal = host.GetService<IEventDal>();
            eventDal.Store("UserThatWeAreNot", GetSomeEvent());
            var client = host.GetService<TestInternalClient>();
            var result = client.Login(Substitute.For<IUserData>());
            result.IsError().Should().BeFalse();
            result.Result.SessionId.Should().NotBe(null);
            //we should have empty events at this point
            var events = client.GetAllEvents(new DateTime(2019, 10, 6), new DateTime(2019, 11, 1));
            events.IsError().Should().BeFalse();
            events.Result.CalendarEvents.Any().Should().BeFalse("we should not have stored any events");
            //we lets add some new event created by user
            var result1 = client.AddEvent(GetSomeEvent());
            result1.IsError().Should().BeFalse();
            events = client.GetAllEvents(new DateTime(2019, 10, 6), new DateTime(2019, 11, 1));
            events.IsError().Should().BeFalse();
            events.Result.CalendarEvents.Any().Should().BeTrue("We should have some events as we previously added one");
            //--Do operations on event
            var eventToUpdate = events.Result.CalendarEvents.Single();
            var producer = host.GetService<ICalendarEventOccurrenceProducer>();
            //lets update second occurence
            eventToUpdate.CustomEventOccurrences.Add(new CalendarEventOccurrence(
                producer.GetAsOfDates(eventToUpdate, new DateTime(2019, 10, 6), new DateTime(2019, 11, 1))
                    .Skip(1).First().Info, new EventContent {Content = "Updated"}, _time));
            //send update
            client.UpdateEvent(eventToUpdate);
            events = client.GetAllEvents(new DateTime(2019, 10, 6), new DateTime(2019, 11, 1));
            events.IsError().Should().BeFalse();
            client.UpdateEvent(eventToUpdate);
            var eventOccurrences =
                producer.GetAsOfDates(eventToUpdate, new DateTime(2019, 10, 6), new DateTime(2019, 11, 1));
            eventOccurrences.Skip(1).First().Content.Content.Should().Be("Updated");

            var result3 = client.RemoveEvent(events.Result.CalendarEvents.SingleOrDefault());
            result3.IsError().Should().BeFalse();
            //we should not have any events at this point
            events = client.GetAllEvents(new DateTime(2019, 10, 6), new DateTime(2019, 11, 1));
            events.IsError().Should().BeFalse();
            events.Result.CalendarEvents.Any().Should().BeFalse("we should not have stored any events");
            var way = host.GetService<INotificationWay>() as TestNotificationsWay;
            // ReSharper disable once PossibleNullReferenceException
            way.NotificationsCount.Should().BeGreaterOrEqualTo(1);
        }

        [Fact]
        public void WHEN__user_is_logged_in_THAN_session_is_created()
        {
            var host = GetHost();
            var client = host.GetService<TestInternalClient>();
            var result = client.Login(Substitute.For<IUserData>());
            result.IsError().Should().BeFalse();
            result.Result.SessionId.Should().NotBe(null);
            var res1 = client.GetSessionInfo();
            res1.IsError().Should().BeFalse();
            res1.Result.SessionData.Should().NotBe(null);

            var res2 = client.GetAllSessionInfo();
            res2.IsError().Should().BeFalse();
            res2.Result.SessionsData.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void WHEN_someone_raise_request_THAN_request_is_Handled()
        {
            var host = GetHost(new IServiceContract[] {new AuthenticationContract(), new SessionCreateContract()});
            var client = host.GetService<TestInternalClient>();
            var result = client.Login(Substitute.For<IUserData>());
            result.IsError().Should().BeFalse();
        }
    }
}