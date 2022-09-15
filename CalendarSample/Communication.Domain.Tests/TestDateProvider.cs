using System;
using Notifications.Domain;

namespace Integration.Tests
{
    internal class TestDateProvider : IDateTimeProvider
    {
        private readonly DateTime _dt = new DateTime(2019, 10, 6, 8, 19, 59);

        public DateTime GetCurrentDate()
        {
            return _dt.Date;
        }

        public DateTime GetCurrentDateTime()
        {
            return _dt;
        }
    }
}