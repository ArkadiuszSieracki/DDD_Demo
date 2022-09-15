using Communication.Core;

namespace Session.Core.API.CreateSession
{
    public sealed class SessionCreateRequest : ServiceRequest<ISessionCreateProcessor>
    {
        public SessionCreateRequest(string userName, IHost hostInfo)
        {
            UserName = userName;
            HostInfo = hostInfo;
        }

        public string UserName { get; }

        public IHost HostInfo { get; }
    }
}