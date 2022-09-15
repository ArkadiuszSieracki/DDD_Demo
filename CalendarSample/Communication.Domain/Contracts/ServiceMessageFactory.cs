using Communication.Core;

namespace Communication.Domain.Contracts
{
    public class ServiceMessageFactory : IServiceMessageFactory
    {
        public ServiceMessageFactory(IMessageIdentityProvider identityProvider)
        {
            IdentityProvider = identityProvider;
        }

        public IMessageIdentityProvider IdentityProvider { get; }

        public IServiceRequestMessage CreateNewMessage(IServiceRequest request)
        {
            var msg = new ServiceRequestMessage(IdentityProvider.GetNextId(), request);
            return msg;
        }

        public IServiceResponseMessage CreateMessageResponse(IServiceRequestMessage request, IServiceResponse response)
        {
            var msg = new ServiceResponseMessage(request.Id, response);
            return msg;
        }
    }
}