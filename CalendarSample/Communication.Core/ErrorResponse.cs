namespace Communication.Core
{
    public class ErrorResponse : IServiceResponse
    {
        public ErrorResponse(ServiceResult result)
        {
            Result = result;
        }

        public ServiceResult Result { get; }
    }
}