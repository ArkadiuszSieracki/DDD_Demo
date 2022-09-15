using Communication.Core;

namespace Communication.Domain
{
    public interface IServiceResponseMessage : IServiceMessage
    {
        IServiceResponse Response { get; set; }
    }
}