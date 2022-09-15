namespace Communication.Domain.Contracts
{
    public interface ILoadBalancersManager
    {
        void StartBalancing(IConcreteBus bus);
    }
}