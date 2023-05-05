using System;
using System.Linq;
using UnityEngine;

namespace Klem.Utils
{
    
    /// <summary>
    /// This component is used to calculate the bounds of a sprite or a collider 2D.
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
        /// Can be set manually or calculated with the button.
        /// </summary>
        [SerializeField] private Bounds bounds;
        
        /// <summary>
        /// The color of the gizmo to draw the bounding box
        /// </summary>
        [SerializeField] private Color boundsGizmoColor = new Color(0.51f, 1f, 0f);
        
        /// <summary>
        ///  The type of the bounds to calculate.
        /// </summary>
        [SerializeField] private BoundsType boundsType = BoundsType.Sprites;
        
        /// <summary>
        ///  Returns the bounds for use in other scripts. (ex: <see cref="Physics2D.OverlapBox(UnityEngine.Vector2,UnityEngine.Vector2,float)"/>
        /// </summary>
        public Bounds Bounds => bounds;
        
        
        /// <summary>
        ///  Draws the bounds as a gizmo.
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = boundsGizmoColor;
            Gizmos.DrawWireCube( transform.position + bounds.center, bounds.size);
        }


        /// <summary>
        ///  Checks if the bounds are set. Or else it will warn you and the Component would be useless :D
        /// </summary>
        private void OnValidate()
        {
            if (IsNotDefault(bounds)) return;
            Debug.LogWarning("Don't forget to set the boundsProperty!");
        }
        
        private static bool IsNotDefault(Bounds boundsProperty)
        {
            return boundsProperty != default;
        }
        
        
        /// <summary>
        ///  Calculates the bounds of the sprites or colliders.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void CalculateBounds()
        {
            Vector3 boundsCenter;
            Vector3 boundsMin;
            Vector3 boundsMax;
            switch (boundsType)
            {
                
                case BoundsType.Sprites:
                    
                    Debug.Log("Bounds2DComponent:  Calculating bounds for sprites");
                    
                    var sprRenderer = GetComponentsInChildren<SpriteRenderer>();
                    if (sprRenderer.Length == 0)
                    {
                        Debug.LogWarning("No sprites found!");
                        return;
                    }
                    
                    boundsCenter = sprRenderer.Aggregate(Vector3.zero, (current, t) => current + t.bounds.center);
                    boundsCenter /= sprRenderer.Length;
                    bounds.center = boundsCenter - transform.position;
                    
                    boundsMin = sprRenderer[0].bounds.min;
                    boundsMax = sprRenderer[0].bounds.max;
                    
                    foreach (var spr in sprRenderer)
                    {
                        boundsMin = Vector3.Min(boundsMin, spr.bounds.min);
                        boundsMax = Vector3.Max(boundsMax, spr.bounds.max);
                    }
                    
                    bounds.SetMinMax(boundsMin, boundsMax);
                    
                    Debug.Log("Bounds2DComponent:  Bounds calculated!");
                    
                    break;
                
                case BoundsType.Colliders:
                    
                    Debug.Log("Bounds2DComponent:  Calculating bounds for colliders");
                    
                    var colliders = GetComponentsInChildren<Collider2D>();
                    if (colliders.Length == 0)
                    {
                        Debug.LogWarning("No colliders found!");
                        return;
                    }
                    
                    boundsCenter = colliders.Aggregate(Vector3.zero, (current, t) => current + t.bounds.center);
                    boundsCenter /= colliders.Length;
                    bounds.center = boundsCenter - transform.position;
                    
                    boundsMin = colliders[0].bounds.min;
                    boundsMax = colliders[0].bounds.max;
                    
                    foreach (var coll in colliders)
                    {
                        boundsMin = Vector3.Min(boundsMin, coll.bounds.min);
                        boundsMax = Vector3.Max(boundsMax, coll.bounds.max);
                    }
                    
                    bounds.SetMinMax(boundsMin, boundsMax);
                    
                    Debug.Log("Bounds2DComponent:  Bounds calculated!");
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
    }
}