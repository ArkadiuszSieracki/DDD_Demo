using System;

namespace Notifications.Domain
{
    public interface IDateTimeProvider
    {
        DateTime GetCurrentDate();
        DateTime GetCurrentDateTime();
    }
}