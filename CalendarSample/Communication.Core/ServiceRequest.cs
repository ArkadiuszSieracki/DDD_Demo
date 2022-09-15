using System;

namespace Communication.Core
{
    public abstract class ServiceRequest<TProcessorType> : IServiceRequest where TProcessorType : IRequestProcessor
    {
        public Type GetProcessorType()
        {
            return typeof(TProcessorType);
        }
    }
}