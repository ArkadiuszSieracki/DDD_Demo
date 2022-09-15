using Authentication.Core;
using Authorization.Core;
using Communication.Core;

namespace Authorization.Domain
{
    public class AnonymousAuthorization : AuthorizationRequest
    {
        public ServiceResult<bool> IsUserValid(IUserData data)
        {
            return new ServiceResult<bool>(true);
        }
    }
}