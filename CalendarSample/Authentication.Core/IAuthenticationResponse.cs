using Communication.Core;
using Session.Core;

namespace Authentication.Core
{
    public interface IAuthenticationResponse : IServiceResponse
    {
        SessionIdentifier SessionId { get; set; }
    }
}