using System.Collections;
using System.Collections.Generic;
using Klem.Samples.Bounds2DComponentSample.Scripts.Interface;
using Klem.Utils;
using Klem.Utils.ServiceLocator;
using UnityEngine;

namespace Klem.Samples.Bounds2DComponentSample.Scripts
{
    [AddComponentMenu("Klem/Bounds2DUpdateService")]
    public class Bounds2DUpdateService : MonoBehaviour, IBounds2dUpdateService
    {
        [SerializeField] private float updateInterval = 1f;
        private readonly List<Bounds2DComponent> _bounds2DComponents = new List<Bounds2DComponent>();
        private IEnumerator _updateCo;

        private void Awake()
        {
            ServiceLocator.Register<IBounds2dUpdateService>(this);
        }

        private void Start()
        {
            _updateCo = UpdateBounds();
            StartCoroutine(_updateCo);   
        }

        private void OnDestroy()
        {
            StopCoroutine(_updateCo);
            ServiceLocator.Unregister<IBounds2dUpdateService>(this);
        }
        
        private IEnumerator UpdateBounds()
        {
            while (Application.isPlaying)
            {
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < _bounds2DComponents.Count; i++)
                {
                    _bounds2DComponents[i].CalculateBounds();
                    yield return new WaitForSeconds(updateInterval / _bounds2DComponents.Count);
                }
                
                yield return new WaitForFixedUpdate();
            }
        }

        public void Register(Bounds2DComponent bounds2DComponent)
        {
            _bounds2DComponents.Add(bounds2DComponent);
        }

        public void Unregister(Bounds2DComponent bounds2DComponent)
        {
            _bounds2DComponents.Remove(bounds2DComponent);
        }
    }
}