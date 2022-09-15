using System.Collections.Generic;

namespace Session.Core
{
    public interface ISessionDal
    {
        void Store(SessionData data);
        SessionData GetSessionData(SessionIdentifier id);

        IEnumerable<SessionData> GetSessionsData();
    }
}