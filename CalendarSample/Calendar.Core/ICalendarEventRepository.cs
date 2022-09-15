using System.Collections.Generic;
using Communication.Core;

namespace Calendar.Core
{
    public interface ICalendarEventRepository
    {
        ServiceResult AddCalendarEvent(string userid, CalendarEvent eEvent);

        ServiceResult UpdateCalendarEvent(string userId, CalendarEvent eEvent);

        ServiceResult DeleteCalendarEvent(string userId, CalendarEvent eEvent);

        ServiceResult<List<CalendarEvent>> GetCalendarEvent(string userId);
        IEnumerable<(string user, CalendarEvent calendarEvent)> GetAllCalendarInfo();
    }
}