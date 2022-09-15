using Communication.Core;

namespace Session.Core.API.CreateSession
{
    public class SessionCreateResponse : IServiceResponse
    {
        public SessionCreateResponse(SessionIdentifier id)
        {
            SessionIdentifier = id;
        }

        public SessionIdentifier SessionIdentifier { get; }
    }
}