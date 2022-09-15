using Communication.Core;

namespace Authentication.Core
{
    public class AuthenticationContract : ServiceContract<AuthenticationRequest, IAuthenticationResponse,
        IAuthenticationProcessor>
    {
    }
}