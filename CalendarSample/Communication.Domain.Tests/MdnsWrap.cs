using System;
using System.Collections.Generic;
using Communication.Core;

namespace Integration.Tests
{
    internal class MdnsWrap : IObservable<IHost>, IHostObservable
    {
        private readonly List<IObserver<IHost>> _hostList = new List<IObserver<IHost>>();
        private readonly List<IHost> _knownHosts = new List<IHost>();

        public IEnumerable<IHost> GetHosts()
        {
            return _knownHosts.ToArray();
        }

        public void Announce(IHost host)
        {
            _knownHosts.Add(host);
            foreach (var observer in _hostList)
            {
                observer.OnNext(host);
            }
        }

        public IDisposable Subscribe(IObserver<IHost> observer)
        {
            return new Unsubscriber<IHost>(_hostList, observer);
        }
    }
}