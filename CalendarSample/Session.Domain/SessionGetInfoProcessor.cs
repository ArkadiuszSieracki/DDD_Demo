using System;
using Communication.Core;
using Session.Core;
using Session.Core.API.GetSessionInfo;

namespace Session.Domain
{
    public class SessionGetInfoProcessor : ISessionGetInfoProcessor
    {
        private readonly ISessionRepository _repository;

        public SessionGetInfoProcessor(ISessionRepository repository)
        {
            _repository = repository;
        }

        public IServiceResponse Process(IServiceRequest serviceRequest)
        {
            var request = serviceRequest as SessionGetInfoRequest;
            if (request == null)
            {
                return new ErrorResponse(new ServiceResult($"{nameof(SessionGetInfoProcessor)}:{nameof(this.Process)}:InvalidRequest", new Exception()));
            }
            return new SessionGetInfoResponse(_repository.GetInfo(request.SessionIdentifier));
        }
    }
}