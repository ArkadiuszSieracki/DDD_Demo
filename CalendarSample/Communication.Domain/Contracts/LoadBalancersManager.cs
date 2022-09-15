using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Communication.Core;

namespace Communication.Domain.Contracts
{
    public class LoadBalancersManager : ILoadBalancersManager
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly IObservable<IHost> _hostObservable;
        private readonly Dictionary<IServiceContract, LoadBalancer> _balancers;

        public LoadBalancersManager(IEnumerable<IServiceContract> contracts, IObservable<IHost> hostObservable,
            IHostObservable observable, IServiceMessageFactory factory)
        {
            _hostObservable = hostObservable;
            _balancers = contracts.ToDictionary(o => o,
                contract => new LoadBalancer(contract, _hostObservable, observable, factory));
        }

        public void StartBalancing(IConcreteBus bus)
        {
            foreach (var balancer in _balancers.Values)
            {
                balancer.Start(bus);
            }
        }

        private class LoadBalancer : IObserver<IHost>
        {
            private static readonly Random Random = new Random();
            private readonly IServiceContract _contract;
            private readonly IServiceMessageFactory _factory;
            private readonly IDisposable _subscription;

            public readonly List<IHost> ContractHandlers = new List<IHost>();

            public LoadBalancer(IServiceContract contract, IObservable<IHost> observable,
                IHostObservable hostObservable, IServiceMessageFactory factory) : this(contract)
            {
                _factory = factory;
                _subscription = observable.Subscribe(this);
                foreach (var host in hostObservable.GetHosts())
                {
                    OnNext(host);
                }
            }

            public LoadBalancer(IServiceContract contract)
            {
                _contract = contract;
            }

            public void OnCompleted()
            {
                _subscription.Dispose();
            }

            public void OnError(Exception error)
            {
            }

            public void OnNext(IHost value)
            {
                if (value.CanHandle(_contract))
                {
                    ContractHandlers.Add(value);
                }
            }

            private bool _should_work = true;
            public void Start(IConcreteBus bus)
            {
                Task.Run(async () =>
                {
                    while (_should_work)
                    {
                        await Task.Delay(100);
                        var requests = bus.GetRequests(_contract);
                        foreach (var request in requests.Result)
                        {
                            // ReSharper disable once AssignmentIsFullyDiscarded
                            _ = Task.Run(() =>
                            {
                                var index = Random.Next(ContractHandlers.Count);
                                var result = ContractHandlers[index].Process(_contract, request.Request);
                                if (result.IsError())
                                {
                                    bus.Send(_factory.CreateMessageResponse(request, new ErrorResponse(result)));
                                }

                                bus.Send(_factory.CreateMessageResponse(request, result.Result));
                            });
                        }
                    }
                });
            }
        }
    }
}