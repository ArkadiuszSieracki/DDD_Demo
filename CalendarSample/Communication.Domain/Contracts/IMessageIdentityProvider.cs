namespace Communication.Domain.Contracts
{
    public interface IMessageIdentityProvider
    {
        MessageIdentity GetNextId();
    }
}