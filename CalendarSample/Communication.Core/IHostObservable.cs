using System.Collections.Generic;

namespace Communication.Core
{
    public interface IHostObservable
    {
        IEnumerable<IHost> GetHosts();
        void Announce(IHost host);
    }
}