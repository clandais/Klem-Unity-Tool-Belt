#if UNITY_EDITOR

#region

using Klem.Utils;
using UnityEditor;
using UnityEngine;

#endregion

namespace Klem.Editor
{
    [CustomEditor(typeof(Bounds2DComponent))]
    public class Bounds2DEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var bounds2DComponent = (Bounds2DComponent)target;
            if (GUILayout.Button("Calculate Bounds")) bounds2DComponent.CalculateBounds();
            base.OnInspectorGUI();
        }
    }
}
#endif