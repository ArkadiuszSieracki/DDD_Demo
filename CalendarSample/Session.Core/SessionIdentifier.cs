using System;

namespace Session.Core
{
    public class SessionIdentifier
    {
        private readonly Guid _id = Guid.NewGuid();

        public override bool Equals(object obj)
        {
            if (obj is SessionIdentifier objT)
            {
                return _id.Equals(objT._id);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
    }
}