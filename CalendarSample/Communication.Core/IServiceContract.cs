using System;

namespace Communication.Core
{
    public interface IServiceContract
    {
        Type GetRequestType();
        Type GetResponseType();

        ServiceResult Validate(IServiceRequest request);
    }
}