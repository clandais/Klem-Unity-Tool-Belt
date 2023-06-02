using Klem.Utils.ServiceLocator;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Klem.Samples.ServiceLocatorSample
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthSprite;
        
        private IHealthService _healthService;
        private int _maxHealth;
        
        
        private void Start()
        {
            _healthService = ServiceLocator.Get<IHealthService>();
            _maxHealth = _healthService.Health.Value;
            _healthService.Health.OnValueChanged += OnHealthChanged;
        }

        private void OnHealthChanged(int oldValue, int newValue)
        {
            Debug.Log($"Health changed from {oldValue} to {newValue}");
            
            if (newValue <= 0)
            {
                Debug.Log("Player died");
                healthSprite.fillAmount = 0;
                return;
            }
            
            healthSprite.fillAmount = (float)newValue / _maxHealth;
            
        }
    }
}
