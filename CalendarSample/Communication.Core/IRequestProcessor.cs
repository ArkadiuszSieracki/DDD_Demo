namespace Communication.Core
{
    public interface IRequestProcessor
    {
        IServiceResponse Process(IServiceRequest serviceRequest);
    }
}