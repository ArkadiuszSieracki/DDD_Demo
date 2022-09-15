using System.Collections.Generic;
using System.Linq;
using Session.Core;

namespace Integration.Tests
{
    internal class TestSessionDal : ISessionDal
    {
        private readonly Dictionary<SessionIdentifier, SessionData> _repo =
            new Dictionary<SessionIdentifier, SessionData>();

        public void Store(SessionData data)
        {
            _repo[data.Identifier] = data;
        }

        public SessionData GetSessionData(SessionIdentifier id)
        {
            if (_repo[id] == null)
            {
            }

            return _repo[id];
        }

        public IEnumerable<SessionData> GetSessionsData()
        {
            return _repo.Values.ToArray();
        }
    }
}