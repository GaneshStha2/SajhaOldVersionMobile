using System;
using System.Collections.Generic;
using System.Text;

namespace Riddha.CourierMS.Common.Services
{
    public class ServiceResult<T>
    {
        public MobileResultStatus Status { get; set; }
        public T Data { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
    public enum MobileResultStatus
    {
        InvalidToken,
        Ok,
        ParameterError,
        DatabaseConnectionError,
        ProcessError,
        UnhandledException
    }
}
