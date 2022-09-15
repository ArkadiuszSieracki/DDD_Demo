using Communication.Core;
using Communication.Domain.Contracts;

namespace Communication.Domain
{
    public class ServiceRequestMessage : IServiceRequestMessage
    {
        public ServiceRequestMessage(MessageIdentity id, IServiceRequest request)
        {
            this.Id = id;
            Request = request;
        }

        public MessageIdentity Id { get; set; }
        public IServiceRequest Request { get; }
    }
}