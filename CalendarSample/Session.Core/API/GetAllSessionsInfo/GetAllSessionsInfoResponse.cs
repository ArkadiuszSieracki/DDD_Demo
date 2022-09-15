using System.Collections.Generic;
using Communication.Core;

namespace Session.Core.API.GetAllSessionsInfo
{
    public class GetAllSessionsInfoResponse : IServiceResponse
    {
        public IEnumerable<SessionData> SessionsData;

        public GetAllSessionsInfoResponse(IEnumerable<SessionData> data)
        {
            SessionsData = data;
        }
    }
}