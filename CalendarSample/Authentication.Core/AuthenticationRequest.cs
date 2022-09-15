using Communication.Core;

namespace Authentication.Core
{
    public class AuthenticationRequest : ServiceRequest<IAuthenticationProcessor>
    {
        public AuthenticationRequest(IUserData data)
        {
            UserData = data;
        }

        public IUserData UserData { get; set; }
    }
}