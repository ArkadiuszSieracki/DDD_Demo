using System;

namespace Communication.Core
{
    public class ServiceContract<TServiceRequestType, TResponseType, TProcessorType> : IServiceContract
        where TServiceRequestType : ServiceRequest<TProcessorType>
        where TResponseType : IServiceResponse
        where TProcessorType : IRequestProcessor
    {
        public Type GetRequestType()
        {
            return typeof(TServiceRequestType);
        }

        public Type GetResponseType()
        {
            return typeof(TResponseType);
        }

        public ServiceResult Validate(IServiceRequest request)
        {
            if (request is TServiceRequestType)
            {
                return ServiceResult.Ok;
            }

            return new ServiceResult($"{GetType()}:InvalidRequestExecuted:{request.GetType()}", new Exception());
        }

        internal ServiceResult<IServiceResponse> Process(IRequestProcessor processor, IServiceRequest request)
        {
            return new ServiceResult<IServiceResponse>(processor.Process(request));
        }
    }
}