namespace Communication.Core
{
    public interface ILoadBalancer<TContract>
    {
        void HandleRequest(TContract contract);
    }
}