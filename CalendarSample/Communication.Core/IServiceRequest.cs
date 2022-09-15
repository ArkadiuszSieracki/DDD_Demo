using System;

namespace Communication.Core
{
    public interface IServiceRequest
    {
        Type GetProcessorType();
    }
}