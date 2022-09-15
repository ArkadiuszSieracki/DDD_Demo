using Communication.Core;

namespace Communication.Domain
{
    public interface IServiceRequestMessage : IServiceMessage
    {
        IServiceRequest Request { get; }
    }
}