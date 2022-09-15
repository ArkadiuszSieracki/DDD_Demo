using Authorization.Core;
using Communication.Core;

namespace Authorization.Domain
{
    public class AnonymousAuthorizationResponse : IAuthorizationResponse
    {
        public ServiceResult<bool> IsAuthorized { get; set; }
    }
}