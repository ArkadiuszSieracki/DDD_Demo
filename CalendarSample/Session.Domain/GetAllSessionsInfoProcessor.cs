using Communication.Core;
using Session.Core;
using Session.Core.API.GetAllSessionsInfo;

namespace Session.Domain
{
    public class GetAllSessionsInfoProcessor : IGetAllSessionsInfoProcessor
    {
        private readonly ISessionRepository _repository;

        public GetAllSessionsInfoProcessor(ISessionRepository repository)
        {
            _repository = repository;
        }

        public IServiceResponse Process(IServiceRequest serviceRequest)
        {
            return new GetAllSessionsInfoResponse(_repository.GetAllSessionsInfo());
        }
    }
}