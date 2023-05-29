using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Klem.Samples.Bounds2DComponentSample.Scripts.Interface;
using UnityEngine;

namespace Klem.Utils
{
    /// <summary>
    ///     This component is used to calculate the bounds of a sprite or a collider 2D.
    /// </summary>
    [AddComponentMenu("Klem/Bounds2DComponent")]
    public class Bounds2DComponent : MonoBehaviour
    {
        private enum BoundsType
        {
            Sprites,
            Colliders
        }

        /// <summary>
        ///     Can be set manually or calculated with the button.
        /// </summary>
        [SerializeField] private Bounds bounds;

        /// <summary>
        ///     The color of the gizmo to draw the bounding box
        /// </summary>
        [SerializeField] private Color boundsGizmoColor = new(0.51f, 1f, 0f);

        /// <summary>
        ///     The type of the bounds to calculate.
        /// </summary>
        [SerializeField] private BoundsType boundsType = BoundsType.Sprites;

        
        /// <summary>
        ///    If true, the bounds will be updated every <see cref="updateBoundsInterval" /> seconds.
        /// </summary>
        [SerializeField] private bool updateBounds = true;
        
        /// <summary>
        ///   The interval in seconds to update the bounds.
        /// </summary>
        [SerializeField] private float updateBoundsInterval = 1f;
        
        /// <summary>
        ///  Shall we log stuff in the console?
        /// </summary>
        [SerializeField] private bool debugLog = false;
        
        /// <summary>
        ///     Returns the bounds for use in other scripts. (ex:
        ///     <see cref="Physics2D.OverlapBox(UnityEngine.Vector2,UnityEngine.Vector2,float)" />
        /// </summary>
        public Bounds Bounds => bounds;


        /// <summary>
        ///     Draws the bounds as a gizmo.
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = boundsGizmoColor;
            Gizmos.DrawWireCube(transform.position + bounds.center, bounds.size);
        }

        #if UNITY_EDITOR

        /// <summary>
        ///     Checks if the bounds are set. Or else it will warn you and the Component would be useless :D
        /// </summary>
        private void OnValidate()
        {
            if (IsNotDefault(bounds)) return;
            Debug.LogWarning("Don't forget to set the boundsProperty! Setting up automatically...");
            CalculateBounds();
        }
        
        #endif

        private IEnumerator _updateCo;

        private IBounds2dUpdateService _bounds2DUpdateService;
        
        private void Start()
        {
            if (ServiceLocator.ServiceLocator.TryGet(out _bounds2DUpdateService))
            {
                _bounds2DUpdateService.Register(this);
                return;
            }
            if (!updateBounds) return;
            _updateCo = UpdateBounds();
            StartCoroutine(_updateCo);
        }

        private IEnumerator UpdateBounds()
        {
            while (Application.isPlaying)
            {
                CalculateBounds();
                yield return new WaitForSeconds(updateBoundsInterval);
            }
        }

        private void OnDestroy()
        {
            if (_updateCo != null)
                StopCoroutine(_updateCo);
        }


        private static bool IsNotDefault(Bounds boundsProperty)
        {
            return boundsProperty != default;
        }


        /// <summary>
        ///     Calculates the bounds of the sprites or colliders.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void CalculateBounds()
        {
            Vector3 boundsCenter;
            Vector3 boundsMin;
            Vector3 boundsMax;
            var position = transform.position;
            switch (boundsType)
            {
                case BoundsType.Sprites:

                    if(debugLog) Debug.Log("Bounds2DComponent:  Calculating bounds for sprites");


                    var sprRenderer = GetComponentsInChildren<SpriteRenderer>();
                    if (sprRenderer.Length == 0)
                    {
                        Debug.LogWarning("No sprites found!");
                        return;
                    }

                    boundsCenter = sprRenderer.Aggregate(
                        Vector3.zero,
                        (current, t) =>
                            current + t.bounds.center - position);
                    boundsCenter /= sprRenderer.Length;
                    bounds.center = boundsCenter;

                    boundsMin = sprRenderer[0].bounds.min;
                    boundsMax = sprRenderer[0].bounds.max;

                    foreach (var spr in sprRenderer)
                    {
                        boundsMin = Vector3.Min(boundsMin, spr.bounds.min);
                        boundsMax = Vector3.Max(boundsMax, spr.bounds.max);
                    }

                    bounds.min = boundsMin - position;
                    bounds.max = boundsMax - position;

                    if (debugLog) Debug.Log("Bounds2DComponent:  Bounds calculated!");

                    break;

                case BoundsType.Colliders:

                    if (debugLog) Debug.Log("Bounds2DComponent:  Calculating bounds for colliders");

                    var colliders = GetComponentsInChildren<Collider2D>();
                    if (colliders.Length == 0)
                    {
                        Debug.LogWarning("No colliders found!");
                        return;
                    }

                    boundsCenter = colliders.Aggregate(
                        Vector3.zero,
                        (current, t)
                            => current + t.bounds.center - position);
                    boundsCenter /= colliders.Length;
                    bounds.center = boundsCenter;

                    boundsMin = colliders[0].bounds.min;
                    boundsMax = colliders[0].bounds.max;

                    foreach (var coll in colliders)
                    {
                        boundsMin = Vector3.Min(boundsMin, coll.bounds.min);
                        boundsMax = Vector3.Max(boundsMax, coll.bounds.max);
                    }

                    bounds.min = boundsMin - position;
                    bounds.max = boundsMax - position;

                    if (debugLog) Debug.Log("Bounds2DComponent:  Bounds calculated!");

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        
    }
}