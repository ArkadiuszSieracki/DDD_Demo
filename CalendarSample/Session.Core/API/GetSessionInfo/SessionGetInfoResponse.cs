using Communication.Core;

namespace Session.Core.API.GetSessionInfo
{
    public class SessionGetInfoResponse : IServiceResponse
    {
        public SessionData SessionData;

        public SessionGetInfoResponse(SessionData data)
        {
            SessionData = data;
        }
    }
}