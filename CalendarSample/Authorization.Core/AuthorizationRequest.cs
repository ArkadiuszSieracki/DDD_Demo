using Authentication.Core;
using Communication.Core;

namespace Authorization.Core
{
    public abstract class AuthorizationRequest : ServiceRequest<IAuthorizationProcessor>
    {
        public IUserData Data { get; set; }
    }
}