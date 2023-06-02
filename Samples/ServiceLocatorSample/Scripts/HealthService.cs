using System;
using Klem.Utils;
using Klem.Utils.ServiceLocator;
using UnityEngine;

namespace Klem.Samples.ServiceLocatorSample
{
    public class HealthService : MonoBehaviour, IHealthService
    {
        
        public ObservableVariable<int> Health { get; private set;}


        private void Awake()
        {
            Health = new ObservableVariable<int>(100);
            ServiceLocator.Register<IHealthService>(this);
        }


        public void TakeDamage(int damage)
        {
            Health.Value -= damage;
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<IHealthService>(this);
        }
    }
}