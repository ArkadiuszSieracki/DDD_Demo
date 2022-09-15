using System;
using Communication.Core;

namespace Session.Core
{
    public class SessionData
    {
        public SessionIdentifier Identifier { get; } = new SessionIdentifier();
        public string Name { get; set; }
        public IHost Host { get; set; }

        public DateTime Started { get; } = DateTime.UtcNow;
    }
}