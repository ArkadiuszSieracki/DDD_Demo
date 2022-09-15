using System.Collections.Generic;
using Calendar.Core;
using Communication.Core;

namespace Calendar.Domain
{
    public class EventRepository : ICalendarEventRepository
    {
        private readonly IEventDal _dal;

        public EventRepository(IEventDal dal)
        {
            _dal = dal;
        }


        public ServiceResult AddCalendarEvent(string userId, CalendarEvent calendarEvent)
        {
            _dal.Store(userId, calendarEvent);
            return ServiceResult.Ok;
        }

        public ServiceResult<List<CalendarEvent>> GetCalendarEvent(string userId)
        {
            return new ServiceResult<List<CalendarEvent>>(_dal.GetUserEvents(userId));
        }

        public IEnumerable<(string user, CalendarEvent calendarEvent)> GetAllCalendarInfo()
        {
            foreach (var item in _dal.GetAllEvents())
            {
                yield return item;
            }
        }

        public ServiceResult UpdateCalendarEvent(string userId, CalendarEvent calendarEvent)
        {
            _dal.Update(userId, calendarEvent);
            return ServiceResult.Ok;
        }

        public ServiceResult DeleteCalendarEvent(string userId, CalendarEvent calendarEvent)
        {
            _dal.Delete(userId, calendarEvent);
            return ServiceResult.Ok;
        }
    }
}