using System;
using System.Threading;
using Authentication.Core;
using Calendar.Core;
using Calendar.Core.API.AddUserEvent;
using Calendar.Core.API.GetAllUserEvents;
using Calendar.Core.API.RemoveUserEvent;
using Calendar.Core.API.UpdateUserEvent;
using Communication.Core;
using Session.Core;
using Session.Core.API.GetAllSessionsInfo;
using Session.Core.API.GetSessionInfo;

namespace Integration.Tests
{
    public class TestInternalClient
    {
        private readonly IMessageBus _bus;

        private SessionIdentifier _sessionIdentifier;

        public TestInternalClient(IMessageBus bus)
        {
            _bus = bus;
        }

        public ServiceResult<IAuthenticationResponse> Login(IUserData data)
        {
            var source = new CancellationTokenSource();
            source.CancelAfter(TimeSpan.FromMilliseconds(5000));

            var result = _bus
                .ExecuteContractAsync<AuthenticationContract, AuthenticationRequest, IAuthenticationResponse,
                    IAuthenticationProcessor>(new AuthenticationContract(), new AuthenticationRequest(data),
                    source.Token).ConfigureAwait(false).GetAwaiter().GetResult();
            if (result.IsError() == false)
            {
                _sessionIdentifier = result.Result.SessionId;
            }

            return result;
        }

        public ServiceResult<SessionGetInfoResponse> GetSessionInfo()
        {
            var source = new CancellationTokenSource();
            source.CancelAfter(TimeSpan.FromMilliseconds(5000));

            var result = _bus
                .ExecuteContractAsync<SessionGetInfoContract, SessionGetInfoRequest, SessionGetInfoResponse,
                    ISessionGetInfoProcessor>(new SessionGetInfoContract(),
                    new SessionGetInfoRequest(_sessionIdentifier), source.Token).ConfigureAwait(false).GetAwaiter()
                .GetResult();
            return result;
        }

        public ServiceResult<GetAllSessionsInfoResponse> GetAllSessionInfo()
        {
            var source = new CancellationTokenSource();
            source.CancelAfter(TimeSpan.FromMilliseconds(5000));

            var result = _bus
                .ExecuteContractAsync<GetAllSessionsInfoContract, GetAllSessionsInfoRequest, GetAllSessionsInfoResponse,
                    IGetAllSessionsInfoProcessor>(new GetAllSessionsInfoContract(), new GetAllSessionsInfoRequest(),
                    source.Token).ConfigureAwait(false).GetAwaiter().GetResult();
            return result;
        }

        public ServiceResult<GetAllUserEventsResponse> GetAllEvents(DateTime startDate, DateTime endDate)
        {
            var source = new CancellationTokenSource();
            source.CancelAfter(TimeSpan.FromMilliseconds(5000));

            var result = _bus
                .ExecuteContractAsync<GetAllUserEventsContract, GetAllUserEventsRequest, GetAllUserEventsResponse,
                    IGetAllUserEventsProcessor>(new GetAllUserEventsContract(),
                    new GetAllUserEventsRequest(_sessionIdentifier, startDate, endDate), source.Token)
                .ConfigureAwait(false).GetAwaiter().GetResult();
            return result;
        }

        public ServiceResult AddEvent(CalendarEvent someCalendarEvent)
        {
            var source = new CancellationTokenSource();
            source.CancelAfter(TimeSpan.FromMilliseconds(5000));

            var result = _bus
                .ExecuteContractAsync<AddUserEventContract, AddUserEventRequest, AddUserEventResponse,
                    IAddUserEventProcessor>(new AddUserEventContract(),
                    new AddUserEventRequest(_sessionIdentifier, someCalendarEvent), source.Token).ConfigureAwait(false)
                .GetAwaiter().GetResult();
            return result;
        }

        public ServiceResult RemoveEvent(CalendarEvent calendarEvent)
        {
            var source = new CancellationTokenSource();
            source.CancelAfter(TimeSpan.FromMilliseconds(5000));

            var result = _bus
                .ExecuteContractAsync<RemoveUserEventContract, RemoveUserEventRequest, RemoveUserEventResponse,
                    IRemoveUserEventProcessor>(new RemoveUserEventContract(),
                    new RemoveUserEventRequest(_sessionIdentifier, calendarEvent), source.Token).ConfigureAwait(false)
                .GetAwaiter().GetResult();
            return result;
        }

        public ServiceResult UpdateEvent(CalendarEvent calendarEvent)
        {
            var source = new CancellationTokenSource();
            source.CancelAfter(TimeSpan.FromMilliseconds(5000));

            var result = _bus
                .ExecuteContractAsync<UpdateUserEventContract, UpdateUserEventRequest, UpdateUserEventResponse,
                    IUpdateUserEventProcessor>(new UpdateUserEventContract(),
                    new UpdateUserEventRequest(_sessionIdentifier, calendarEvent), source.Token).ConfigureAwait(false)
                .GetAwaiter().GetResult();
            return result;
        }
    }
}