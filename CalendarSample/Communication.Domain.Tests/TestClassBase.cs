using System;
using System.Collections.Generic;
using Autofac;
using Calendar.Domain;
using Communication.Core;
using Communication.Domain;
using Communication.Domain.Contracts;
using Communication.Infrastructure.Sample;
using Notifications.Domain;
using Session.Core;

namespace Integration.Tests
{
    public class TestClassBase
    {
        private readonly ServiceLocator _locator;
        private IHostObservable _wrap;

        public TestClassBase(bool manualInit = false)
        {
            _locator = new ServiceLocator();

            if (manualInit == false)
                // ReSharper disable once VirtualMemberCallInConstructor
            {
                Initialize();
            }
        }

        public virtual void Initialize()
        {
            _locator.Builder.RegisterType<TestInternalClient>().InstancePerLifetimeScope();
            _locator.Builder.RegisterType<MessageBusSample>().As<IConcreteBus>().SingleInstance();
            _locator.Builder.RegisterType<MdnsWrap>().As<IObservable<IHost>>().As<IHostObservable>().SingleInstance();
            _locator.Builder.RegisterType<TestSessionDal>().As<ISessionDal>().SingleInstance();
            _locator.Builder.RegisterType<TestEventDal>().As<IEventDal>().SingleInstance();
            _locator.Builder.RegisterType<TestDateProvider>().As<IDateTimeProvider>();
            _locator.Builder.RegisterType<TestNotificationsWay>().As<INotificationWay>().SingleInstance();
            _locator.Build();
            var ctx = _locator.GetHostContext();
            _wrap = ctx.Resolve<IHostObservable>();
        }

        protected Host GetHost(IServiceContract[] contracts)
        {
            var host = new Host(_locator.GetHostContext());
            foreach (var contract in contracts)
            {
                host.AddHandle(contract);
            }

            _wrap.Announce(host);
            return host;
        }

        protected Host GetHost()
        {
            var ctx = _locator.GetHostContext();
            var host = new Host(ctx);
            foreach (var contract in ctx.Resolve<IEnumerable<IServiceContract>>())
            {
                host.AddHandle(contract);
            }

            _wrap.Announce(host);
            return host;
        }
    }
}