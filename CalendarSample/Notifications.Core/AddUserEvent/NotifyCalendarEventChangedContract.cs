using Communication.Core;

namespace Notifications.Core.AddUserEvent
{
    public class NotifyCalendarEventChangedContract : ServiceContract<NotifyCalendarEventChangedRequest,
        NotifyCalendarEventChangedResponse, INotifyCalendarEventChangedProcessor>
    {
    }
}