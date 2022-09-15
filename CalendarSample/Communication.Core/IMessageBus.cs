using System.Threading;
using System.Threading.Tasks;

namespace Communication.Core
{
    public interface IMessageBus
    {
        Task<ServiceResult<TResponseType>> ExecuteContractAsync<TServiceContract, TServiceRequestType, TResponseType,
            TProcessorType>(TServiceContract contract, TServiceRequestType request, CancellationToken token)
            where TServiceContract : ServiceContract<TServiceRequestType, TResponseType, TProcessorType>
            where TServiceRequestType : ServiceRequest<TProcessorType>
            where TResponseType : IServiceResponse
            where TProcessorType : IRequestProcessor;
    }
}