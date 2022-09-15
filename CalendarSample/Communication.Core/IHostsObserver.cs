using System;

namespace Communication.Core
{
    public interface IHostsObserver : IObserver<IHost>
    {
        void Announce(IHost host);
    }
}