using Communication.Core;

namespace Communication.Domain.Contracts
{
    public interface IServiceMessageFactory
    {
        IServiceRequestMessage CreateNewMessage(IServiceRequest request);
        IServiceResponseMessage CreateMessageResponse(IServiceRequestMessage request, IServiceResponse response);
    }
}