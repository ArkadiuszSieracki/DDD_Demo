using System.Collections.Generic;
using Communication.Core;

namespace Session.Core
{
    public interface ISessionRepository
    {
        SessionIdentifier CreateSessionData(string userId, IHost host);

        ServiceResult HeartBeat(SessionIdentifier identifier);
        SessionData GetInfo(SessionIdentifier requestSessionIdentifier);

        IEnumerable<SessionData> GetAllSessionsInfo();
    }
}