namespace Communication.Domain.Contracts
{
    public class MessageIdentityProvider : IMessageIdentityProvider
    {
        private int _id;

        public MessageIdentity GetNextId()
        {
            return new MessageIdentity(++_id);
        }
    }
}