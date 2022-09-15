using System;

namespace Notifications.Domain
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentDate()
        {
            return DateTime.UtcNow.Date;
        }

        public DateTime GetCurrentDateTime()
        {
            return DateTime.UtcNow;
        }
    }
}