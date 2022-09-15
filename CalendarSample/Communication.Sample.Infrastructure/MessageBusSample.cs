using System.Collections.Generic;
using System.Linq;
using Communication.Core;
using Communication.Domain;
using Communication.Domain.Contracts;

namespace Communication.Infrastructure.Sample
{
    public class MessageBusSample : IConcreteBus
    {
        private readonly object _interlock = new object();
        private List<IServiceMessage> _messages = new List<IServiceMessage>();

        ServiceResult IConcreteBus.Send(IServiceMessage message)
        {
            _messages.Add(message);
            return ServiceResult.Ok;
        }

        ServiceResult<IServiceResponseMessage> IConcreteBus.GetResponse(IServiceRequestMessage message)
        {
            var response = _messages.OfType<IServiceResponseMessage>().SingleOrDefault(o => Equals(o.Id, message.Id));
            return new ServiceResult<IServiceResponseMessage>(response);
        }

        public ServiceResult<IServiceRequestMessage[]> GetRequests(IServiceContract contract)
        {
            lock (_interlock)
            {
                var tmp = _messages.OfType<IServiceRequestMessage>().ToArray();
                var result = tmp.Where(o => contract.Validate(o.Request).IsError() == false).ToArray();
                _messages = _messages.Where(o => result.Contains(o) == false).ToList();
                return new ServiceResult<IServiceRequestMessage[]>(result);
            }
        }
    }
}