using UnityEditor;

namespace StubbUnity.Physics
{
    [CustomEditor(typeof(ViewPhysics))]
    public class ViewPhysicsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }
}