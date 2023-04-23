using Klem.Utils;
using UnityEditor;
using UnityEngine;

namespace Klem.Core
{
    [CustomEditor(typeof(Bounds2DComponent))]
    public class Bounds2DEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var bounds2DComponent = (Bounds2DComponent) target;
            
            
            if (GUILayout.Button("Calculate Bounds"))
            {
                bounds2DComponent.CalculateBounds();
            }
            
            base.OnInspectorGUI();
        }
    }
}