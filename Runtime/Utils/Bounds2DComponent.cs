using UnityEngine;

namespace Klem.Core.Utils
{
 
    [AddComponentMenu("Klem/Bounds2DComponent")]
    public class Bounds2DComponent : MonoBehaviour
    {
        [SerializeField] private Bounds bounds;
        
        public Bounds Bounds => bounds;
        
        
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.51f, 1f, 0f);
            Gizmos.DrawWireCube( transform.position + bounds.center, bounds.size);
        }


        private void OnValidate()
        {
            if (IsNotDefault(bounds)) return;
            Debug.LogWarning("Don't forget to set the boundsProperty!");
        }

        private bool IsNotDefault(Bounds boundsProperty)
        {
            return boundsProperty != default;
        }
        
        
        [ExecuteInEditMode]
        public void CalculateBounds()
        {
            var sprRenderer = GetComponent<SpriteRenderer>();
            if (!sprRenderer)
                sprRenderer = GetComponentInChildren<SpriteRenderer>();
            
            if (sprRenderer != null)
            {
                bounds = sprRenderer.bounds;
                bounds.center -= transform.position;
            }
            else
            {
                var col = GetComponentInChildren<BoxCollider2D>();
                if (!col) return;
                bounds = col.bounds;
                bounds.center -= transform.position;
            }
        }
        
    }
}