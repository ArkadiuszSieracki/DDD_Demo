using Authentication.Core;
using Session.Core;

namespace Authentication.Domain
{
    public class AuthenticationRespone : IAuthenticationResponse
    {
        public AuthenticationRespone(SessionIdentifier sessionIdentifier)
        {
            SessionId = sessionIdentifier;
        }

        public SessionIdentifier SessionId { get; set; }
    }
}