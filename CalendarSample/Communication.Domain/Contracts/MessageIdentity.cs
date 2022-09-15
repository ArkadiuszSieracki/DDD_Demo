namespace Communication.Domain.Contracts
{
    public class MessageIdentity
    {
        public MessageIdentity(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var objt = obj as MessageIdentity;
            if (objt != null)
            {
                return Id.Equals(objt.Id);
            }

            return false;
        }
    }
}