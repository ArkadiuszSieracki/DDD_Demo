using System.Reflection;
using Authentication.Core;
using Authentication.Domain;
using Authorization.Domain;
using Autofac;
using Calendar.Core;
using Calendar.Domain;
using Communication.Core;
using Communication.Domain.Contracts;
using Communication.Infrastructure.Sample;
using Notifications.Core.AddUserEvent;
using Notifications.Domain;
using Session.Core;
using Session.Domain;

namespace Integration.Tests
{
    public class ServiceLocator
    {
        private IContainer _container;

        public ServiceLocator()
        {
            Assembly.GetExecutingAssembly();

            Builder.RegisterAssemblyTypes(typeof(IMessageBus).Assembly)
                .AsImplementedInterfaces();
            Builder.RegisterAssemblyTypes(typeof(MessageBus).Assembly)
                .AsImplementedInterfaces();
            Builder.RegisterAssemblyTypes(typeof(MessageBusSample).Assembly)
                .AsImplementedInterfaces();
            Builder.RegisterAssemblyTypes(typeof(AnonymousAuthorization).Assembly)
                .AsImplementedInterfaces();
            Builder.RegisterAssemblyTypes(typeof(AuthenticationProcessor).Assembly)
                .AsImplementedInterfaces();
            Builder.RegisterAssemblyTypes(typeof(AuthenticationContract).Assembly)
                .AsImplementedInterfaces();
            Builder.RegisterAssemblyTypes(typeof(ISessionDal).Assembly)
                .AsImplementedInterfaces();
            Builder.RegisterAssemblyTypes(typeof(SessionRepository).Assembly)
                .AsImplementedInterfaces();
            Builder.RegisterAssemblyTypes(typeof(IEventDal).Assembly)
                .AsImplementedInterfaces();
            Builder.RegisterAssemblyTypes(typeof(CalendarEvent).Assembly)
                .AsImplementedInterfaces();
            Builder.RegisterAssemblyTypes(typeof(INotifyCalendarEventChangedProcessor).Assembly)
                .AsImplementedInterfaces();
            Builder.RegisterAssemblyTypes(typeof(NotifyCalendarEventChangedProcessor).Assembly)
                .AsImplementedInterfaces();

            Builder.RegisterType<MessageIdentityProvider>().As<IMessageIdentityProvider>().SingleInstance();
            Builder.RegisterType<LoadBalancersManager>().As<ILoadBalancersManager>();
            Builder.RegisterType<HostsObserver>().As<IHostsObserver>().SingleInstance();
        }

        public ContainerBuilder Builder { get; } = new ContainerBuilder();

        public void Build()
        {
            _container = Builder.Build();
            _container.Resolve<IMessageBus>();
        }

        public ILifetimeScope GetHostContext()
        {
            return _container.BeginLifetimeScope();
        }
    }
}