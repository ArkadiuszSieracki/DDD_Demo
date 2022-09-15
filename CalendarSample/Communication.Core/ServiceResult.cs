using System;
using System.Collections.Generic;
using System.Linq;

namespace Communication.Core
{
    public class ServiceResult
    {
        public ServiceResult()
        {
        }

        protected ServiceResult(string messageKey)
        {
            MessageKey = messageKey;
        }

        public ServiceResult(string messageKey, ServiceResult innerResult)
        {
            MessageKey = messageKey;
            InnerServiceResult = innerResult;
        }

        public ServiceResult(string messageKey, Exception e) : this(messageKey)
        {
            Exceptions.Add(e);
        }

        public string MessageKey { get; set; }
        public List<Exception> Exceptions { get; set; } = new List<Exception>();
        public ServiceResult InnerServiceResult { get; set; }

        public static ServiceResult Ok
        {
            get { return new ServiceResult(); }
        }

        public bool IsError()
        {
            return Exceptions.Any() || InnerServiceResult?.IsError() == true;
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult(string messageKey, ServiceResult innerResult) : base(messageKey, innerResult)
        {
        }

        public ServiceResult(string messageKey, Exception e) : base(messageKey, e)
        {
        }

        public ServiceResult(T result)
        {
            Result = result;
        }

        public T Result { get; set; }
    }
}