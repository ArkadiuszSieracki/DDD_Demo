namespace Communication.Core
{
    public interface IHost
    {
        bool CanHandle(IServiceContract contract);
        ServiceResult<IServiceResponse> Process(IServiceContract contract, IServiceRequest request);
    }
}