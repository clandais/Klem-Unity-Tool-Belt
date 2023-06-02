using Klem.Utils.ServiceLocator;
using UnityEngine;

namespace Klem.Samples.ServiceLocatorSample
{
    public class TakeDamageButton : MonoBehaviour
    {
        private IHealthService _healthService;
        
        private void Start()
        {
            _healthService = ServiceLocator.Get<IHealthService>();
        }
        
        public void TakeDamage()
        {
            _healthService.TakeDamage(10);
        }
    }
}
