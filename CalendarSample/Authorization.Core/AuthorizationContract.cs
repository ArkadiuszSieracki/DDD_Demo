using Communication.Core;

namespace Authorization.Core
{
    internal class
        AuthorizationContract : ServiceContract<AuthorizationRequest, IAuthorizationResponse, IAuthorizationProcessor>
    {
    }
}