using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Communication.Core;

namespace Communication.Domain
{
    public class Host : IHost
    {
        private readonly List<IServiceContract> _contracts = new List<IServiceContract>();


        private readonly ILifetimeScope _scope;

        public Host(ILifetimeScope hostContext)
        {
            _scope = hostContext;
        }

        public bool CanHandle(IServiceContract contract)
        {
            return _contracts.Any(o => o.GetType() == contract.GetType());
        }

        public ServiceResult<IServiceResponse> Process(IServiceContract contract, IServiceRequest request)
        {
            var isValid = contract.Validate(request);
            if (isValid.IsError())
            {
                return new ServiceResult<IServiceResponse>("ServiceResult<IServiceResponse>", isValid);
            }

            var processor = _scope.Resolve(request.GetProcessorType()) as IRequestProcessor;
            if (processor == null)
            {
                return new ServiceResult<IServiceResponse>("Invalid:Contract:Data", new Exception());
            }
            return new ServiceResult<IServiceResponse>(processor.Process(request));
        }

        public T GetService<T>()
        {
            return _scope.Resolve<T>();
        }

        public void AddHandle(IServiceContract contract)
        {
            _contracts.Add(contract);
        }
    }
}