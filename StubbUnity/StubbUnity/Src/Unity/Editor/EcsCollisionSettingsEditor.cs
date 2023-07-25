using StubbUnity.Unity.View;
using UnityEditor;

namespace StubbUnity.Unity.Editor
{
    [CustomEditor(typeof(EcsViewLink), true)]
    public class EcsCollisionSettingsEditor : UnityEditor.Editor
    {
        private bool _hasPhysics;
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }
}