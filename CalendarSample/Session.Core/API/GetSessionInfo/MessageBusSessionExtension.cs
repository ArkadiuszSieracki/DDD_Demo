using System;
using System.Threading;
using Communication.Core;

namespace Session.Core.API.GetSessionInfo
{
    public static class MessageBusSessionExtension
    {
        public static ServiceResult<string> GetUserData(this IMessageBus bus, SessionIdentifier sessionId)
        {
            var source = new CancellationTokenSource();
            source.CancelAfter(TimeSpan.FromMilliseconds(5000));

            var result = bus
                .ExecuteContractAsync<SessionGetInfoContract, SessionGetInfoRequest, SessionGetInfoResponse,
                    ISessionGetInfoProcessor>(new SessionGetInfoContract(), new SessionGetInfoRequest(sessionId),
                    source.Token).ConfigureAwait(false).GetAwaiter().GetResult();
            if (result.IsError())
            {
                return new ServiceResult<string>("Failed to get session info", result);
            }

            return new ServiceResult<string>(result.Result.SessionData.Name);
        }
    }
}