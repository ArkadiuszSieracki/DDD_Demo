using System;
using System.Collections.Generic;
using System.Threading;
using Communication.Core;

namespace Session.Core.API.GetAllSessionsInfo
{
    public static class MessageBusSessionExtensionGetAll
    {
        public static ServiceResult<IEnumerable<SessionData>> GetAllSessionsData(this IMessageBus bus)
        {
            var source = new CancellationTokenSource();
            source.CancelAfter(TimeSpan.FromMilliseconds(5000));

            var result = bus
                .ExecuteContractAsync<GetAllSessionsInfoContract, GetAllSessionsInfoRequest, GetAllSessionsInfoResponse,
                    IGetAllSessionsInfoProcessor>(new GetAllSessionsInfoContract(), new GetAllSessionsInfoRequest(),
                    source.Token).ConfigureAwait(false).GetAwaiter().GetResult();
            if (result.IsError())
            {
                return new ServiceResult<IEnumerable<SessionData>>("Failed to get sessions info", result);
            }

            return new ServiceResult<IEnumerable<SessionData>>(result.Result.SessionsData);
        }
    }
}