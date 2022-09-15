using System;
using System.Collections.Generic;
using System.Linq;
using Calendar.Core;
using Calendar.Domain;

namespace Integration.Tests
{
    public class TestEventDal : IEventDal
    {
        private readonly Dictionary<string, Dictionary<Guid, CalendarEvent>> _repo =
            new Dictionary<string, Dictionary<Guid, CalendarEvent>>();

        public void Store(string userData, CalendarEvent calendarEvent)
        {
            if (!_repo.ContainsKey(userData))
            {
                _repo[userData] = new Dictionary<Guid, CalendarEvent>();
            }

            _repo[userData][calendarEvent.Id] = calendarEvent;
        }

        public void Update(string userData, CalendarEvent calendarEvent)
        {
            Store(userData, calendarEvent);
        }

        public void Delete(string userData, CalendarEvent calendarEvent)
        {
            if (!_repo.ContainsKey(userData))
            {
                return;
            }

            if (_repo[userData].ContainsKey(calendarEvent.Id))
            {
                _repo[userData].Remove(calendarEvent.Id);
            }
        }

        public List<CalendarEvent> GetUserEvents(string userData)
        {
            if (!_repo.ContainsKey(userData))
            {
                return new List<CalendarEvent>();
            }

            return _repo[userData].Values.ToList();
        }

        public IEnumerable<(string user, CalendarEvent calendarEvent)> GetAllEvents()
        {
            foreach (var index1 in _repo)
            foreach (var index2 in index1.Value)
            {
                yield return (index1.Key, index2.Value);
            }
        }
    }
}