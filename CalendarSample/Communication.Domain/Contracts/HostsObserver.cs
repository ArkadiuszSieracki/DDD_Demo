using System;
using System.Collections.Generic;
using Communication.Core;

namespace Communication.Domain.Contracts
{
    public class HostsObserver : IHostsObserver
    {
        private readonly IObservable<IHost> _hostObservable;

        private readonly List<IHost> _hosts = new List<IHost>();

        public HostsObserver(IObservable<IHost> hostObservable)
        {
            _hostObservable = hostObservable;
            _hostObservable.Subscribe(this);
        }

        public void Announce(IHost host)
        {
            _hosts.Add(host);
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(IHost value)
        {
            Announce(value);
        }
    }
}