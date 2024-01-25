using Klem.Utils;
using Klem.Utils.ServiceLocator;

namespace Klem.Samples.ServiceLocatorSample
{
    public interface IHealthService : IGameService
    {
        ObservableVariable<int> Health { get; }
        
        void TakeDamage(int damage);
    }
}