using Communication.Core;

namespace Authentication.Core
{
    public interface IUserData
    {
        string Name { get; }
        IHost HostInfo { get; set; }
    }
}