using Klem.Utils;
using Klem.Utils.ServiceLocator;

namespace Klem.Samples.Bounds2DComponentSample.Scripts.Interface
{
    public interface IBounds2dUpdateService : IGameService
    {
        public void Register(Bounds2DComponent bounds2DComponent);
        
        public void Unregister(Bounds2DComponent bounds2DComponent);
        
        
    }
}