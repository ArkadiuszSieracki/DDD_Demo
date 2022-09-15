using System;
using System.Collections.Generic;
using Communication.Core;
using Session.Core;

namespace Session.Domain
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ISessionDal _dal;

        public SessionRepository(ISessionDal dal)
        {
            _dal = dal;
        }

        public SessionIdentifier CreateSessionData(string userId, IHost host)
        {
            var newSession = new SessionData {Name = userId, Host = host};
            _dal.Store(newSession);
            return newSession.Identifier;
        }

        public ServiceResult HeartBeat(SessionIdentifier identifier)
        {
            throw new NotImplementedException();
        }

        public SessionData GetInfo(SessionIdentifier requestSessionIdentifier)
        {
            return _dal.GetSessionData(requestSessionIdentifier);
        }

        public IEnumerable<SessionData> GetAllSessionsInfo()
        {
            return _dal.GetSessionsData();
        }
    }
}