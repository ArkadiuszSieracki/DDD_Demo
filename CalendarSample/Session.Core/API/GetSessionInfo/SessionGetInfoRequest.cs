using Communication.Core;

namespace Session.Core.API.GetSessionInfo
{
    public class SessionGetInfoRequest : ServiceRequest<ISessionGetInfoProcessor>
    {
        public SessionGetInfoRequest(SessionIdentifier sessionIdentifier)
        {
            SessionIdentifier = sessionIdentifier;
        }

        public SessionIdentifier SessionIdentifier { get; }
    }
}