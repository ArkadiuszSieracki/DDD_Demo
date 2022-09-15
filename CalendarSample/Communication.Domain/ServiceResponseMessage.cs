using Communication.Core;
using Communication.Domain.Contracts;

namespace Communication.Domain
{
    public class ServiceResponseMessage : IServiceResponseMessage
    {
        public ServiceResponseMessage(MessageIdentity id, IServiceResponse response)
        {
            this.Id = id;
            Response = response;
        }

        public MessageIdentity Id { get; set; }
        public IServiceResponse Response { get; set; }
    }
}