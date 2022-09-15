using System;
using System.Threading;
using Authentication.Core;
using Communication.Core;
using Session.Core.API.CreateSession;

namespace Authentication.Domain
{
    public class AuthenticationProcessor : IAuthenticationProcessor
    {
        private readonly IMessageBus _bus;

        public AuthenticationProcessor(IMessageBus bus)
        {
            _bus = bus;
        }

        public IServiceResponse Process(IServiceRequest serviceRequest)
        {
            var source = new CancellationTokenSource();
            var request = serviceRequest as AuthenticationRequest;
            if (request == null)
            {
                return new ErrorResponse(new ServiceResult($"{nameof(AuthenticationProcessor)}:{nameof(this.Process)}:InvalidRequest",new Exception()));
            }
            var response = _bus.ExecuteContractAsync<SessionCreateContract,
                SessionCreateRequest,
                SessionCreateResponse,
                ISessionCreateProcessor>(
                new SessionCreateContract(),
                new SessionCreateRequest(request.UserData.Name, request.UserData.HostInfo),
                source.Token);
            response.Wait(source.Token);

            if (response.Result.IsError())
            {
                return new ErrorResponse(response.Result);
            }

            return new AuthenticationRespone(response.Result.Result.SessionIdentifier);
        }
    }
}