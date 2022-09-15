using Communication.Domain.Contracts;

namespace Communication.Domain
{
    public interface IServiceMessage
    {
        MessageIdentity Id { get; set; }
    }
}