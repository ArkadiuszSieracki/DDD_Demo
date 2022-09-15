using System.Collections.Generic;
using Calendar.Core;

namespace Calendar.Domain
{
    public interface IEventDal
    {
        void Store(string userData, CalendarEvent calendarEvent);
        void Update(string userData, CalendarEvent calendarEvent);
        void Delete(string userData, CalendarEvent calendarEvent);
        List<CalendarEvent> GetUserEvents(string userData);
        IEnumerable<(string user, CalendarEvent calendarEvent)> GetAllEvents();
    }
}