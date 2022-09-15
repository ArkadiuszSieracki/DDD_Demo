using Communication.Core;

namespace Session.Core.API.CreateSession
{
    public sealed class
        SessionCreateContract : ServiceContract<SessionCreateRequest, SessionCreateResponse, ISessionCreateProcessor>
    {
    }
}