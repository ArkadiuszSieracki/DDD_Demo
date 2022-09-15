using Communication.Core;

namespace Session.Core.API.GetSessionInfo
{
    public class
        SessionGetInfoContract : ServiceContract<SessionGetInfoRequest, SessionGetInfoResponse, ISessionGetInfoProcessor
        >
    {
    }
}