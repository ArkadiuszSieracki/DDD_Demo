using System;
using Communication.Core;
using Session.Core;
using Session.Core.API.CreateSession;

namespace Session.Domain
{
    public class CreateSessionProcessor : ISessionCreateProcessor
    {
        private readonly ISessionRepository _repository;

        public CreateSessionProcessor(ISessionRepository repository)
        {
            _repository = repository;
        }

        public IServiceResponse Process(IServiceRequest serviceRequest)
        {
            var request = serviceRequest as SessionCreateRequest;
            if (request == null)
            {
                return new ErrorResponse(new ServiceResult($"{nameof(CreateSessionProcessor)}:{nameof(this.Process)}:InvalidRequest", new Exception()));
            }
            return new SessionCreateResponse(_repository.CreateSessionData(request.UserName, request.HostInfo));
        }
    }
}