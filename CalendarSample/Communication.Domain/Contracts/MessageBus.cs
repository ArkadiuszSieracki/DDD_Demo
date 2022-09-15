using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Communication.Core;

namespace Communication.Domain.Contracts
{
    public class MessageBus : IMessageBus, IObservable<IServiceRequestMessage>
    {
        private readonly IServiceMessageFactory _messageFactory;

        private readonly List<IObserver<IServiceRequestMessage>> _subscribers =
            new List<IObserver<IServiceRequestMessage>>();


        public MessageBus(IConcreteBus bus, IServiceMessageFactory factory, ILoadBalancersManager manager)
        {
            _messageFactory = factory;
            Bus = bus;
            manager.StartBalancing(bus);
        }

        public IConcreteBus Bus { get; }

        public async Task<ServiceResult<TResponseType>> ExecuteContractAsync<TServiceContract, TServiceRequestType,
            TResponseType, TProcessorType>(
            TServiceContract contract, TServiceRequestType request, CancellationToken token)
            where TServiceContract : ServiceContract<TServiceRequestType, TResponseType, TProcessorType>
            where TServiceRequestType : ServiceRequest<TProcessorType>
            where TResponseType : IServiceResponse
            where TProcessorType : IRequestProcessor
        {
            var result = await ExecuteContractAsync(contract, request, token);
            if (result.IsError())
            {
                return new ServiceResult<TResponseType>(
                    $"{nameof(MessageBus)}:{nameof(ExecuteContractAsync)}:Failed:{contract}", result);
            }

            return new ServiceResult<TResponseType>((TResponseType) result.Result);
        }

        IDisposable IObservable<IServiceRequestMessage>.Subscribe(IObserver<IServiceRequestMessage> observer)
        {
            return new Unsubscriber<IServiceRequestMessage>(_subscribers, observer);
        }

        public async Task<ServiceResult<IServiceResponse>> WaitResponseAsync(IServiceRequest request,
            CancellationToken token)
        {
            var message = _messageFactory.CreateNewMessage(request);
            var resp = Bus.Send(message);
            if (resp.IsError())
            {
                return new ServiceResult<IServiceResponse>($"{nameof(MessageBus)}:{nameof(Bus.Send)}:Failed", resp);
            }

            try
            {
                var response = await WaitResponseAsync(message, token);
                return new ServiceResult<IServiceResponse>(response);
            }
            catch (Exception e)
            {
                return new ServiceResult<IServiceResponse>($"{nameof(MessageBus)}:{nameof(WaitResponseAsync)}:Failed",
                    e);
            }
        }

        private async Task<IServiceResponse> WaitResponseAsync(IServiceRequestMessage message, CancellationToken token)
        {
            IServiceResponse result = null;
            while (result == null)
            {
                var response = Bus.GetResponse(message);
                if (response.IsError() || response.Result == null)
                {
                    if (token.IsCancellationRequested)
                    {
                        throw new TaskCanceledException();
                    }

                    await Task.Delay(1000, token);
                }
                else
                {
                    result = response.Result.Response;
                }
            }

            return result;
        }


        public async Task<ServiceResult<IServiceResponse>> ExecuteContractAsync(IServiceContract contract,
            IServiceRequest request,
            CancellationToken token)
        {
            contract.Validate(request);
            return await WaitResponseAsync(request, token);
        }
    }
}