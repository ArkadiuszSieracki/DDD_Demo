using Communication.Core;

namespace Communication.Domain.Contracts
{
    public interface IConcreteBus
    {
        ServiceResult Send(IServiceMessage message);

        ServiceResult<IServiceResponseMessage> GetResponse(IServiceRequestMessage message);
        ServiceResult<IServiceRequestMessage[]> GetRequests(IServiceContract contract);
    }
}